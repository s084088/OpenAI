using AI.Dev.OpenAI.GPT;
using SyNemo.OpenAI.App.Models;
using SyNemo.OpenAI.ChatGPT35.Models;
using SyNemo.OpenAI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyNemo.OpenAI;

/// <summary>
/// 使用了GPT3.5的聊天应用
/// </summary>
public class Chat35
{
    private readonly Chat35Config _config;
    private readonly List<ChatMessageModel> messages = new();

    /// <summary>
    /// 聊天记录
    /// </summary>
    public IEnumerable<ChatMessageModel> Messages => messages.AsReadOnly();

    /// <summary>
    /// 创建聊天
    /// </summary>
    /// <param name="config">聊天配置</param>
    public Chat35(Chat35Config config = null)
    {
        _config = config ?? new();

        if (_config.Messages != null)
            foreach (IChatMessage m in _config.Messages)
                messages.Add(new(m.User, m.Message));
    }

    /// <summary>
    /// 发送信息
    /// </summary>
    /// <param name="ask">你当次讲的话</param>
    /// <returns></returns>
    public async Task<string> Ask(string ask) => await Ask(new DialogueModel(ask));

    /// <summary>
    /// 发送信息
    /// </summary>
    /// <param name="model">当次的讲话模型</param>
    /// <returns></returns>
    public async Task<string> Ask(DialogueModel model)
    {
        ChatMessageModel userMessage = new(ChatRole.User, model.Question);
        List<Message> msgs = GetMessages(userMessage);
        ChatRequest request = GetRequest(msgs);
        ChatResponse response = await ChatGPT35.ChatGPT35.Ask(request);
        ChatMessageModel aiMessage = new(ChatRole.AI, response?.choices[0]?.message?.content?.Trim());

        userMessage.Tokens = response.usage.prompt_tokens;
        aiMessage.Tokens = response.usage.completion_tokens;

        messages.Add(userMessage);
        messages.Add(aiMessage);

        model.Prompt = userMessage.Message;
        model.Answer = aiMessage.Message;
        model.SendToken = userMessage.Tokens;
        model.ReceiveToken = aiMessage.Tokens;

        return model.Answer;
    }

    /// <summary>
    /// 发送信息,不使用上下文
    /// </summary>
    /// <param name="ask">你当次讲的话</param>
    /// <returns></returns>
    public async Task<string> AskWithOutContext(string ask) => await AskWithOutContext(new DialogueModel(ask));

    /// <summary>
    /// 发送信息,不使用上下文
    /// </summary>
    /// <param name="model">你当次的讲话模型</param>
    /// <returns></returns>
    public async Task<string> AskWithOutContext(DialogueModel model)
    {
        List<Message> msgs = GetMessagesWithOutLog(model.Question);
        ChatRequest request = GetRequest(msgs);
        ChatResponse response = await ChatGPT35.ChatGPT35.Ask(request);

        model.Prompt = model.Question;
        model.Answer = response?.choices[0]?.message?.content?.Trim();
        model.SendToken = response.usage.prompt_tokens;
        model.ReceiveToken = response.usage.completion_tokens;

        return model.Answer;
    }

    /// <summary>
    /// 构建入参
    /// </summary>
    /// <param name="_prompt"></param>
    /// <returns></returns>
    private ChatRequest GetRequest(List<Message> _prompt) => new()
    {
        model = Chat35Resource.Model,
        messages = _prompt.ToArray(),
    };

    /// <summary>
    /// 获取新对话的Prompt
    /// </summary>
    /// <param name="ask"></param>
    /// <returns></returns>
    private List<Message> GetMessages(ChatMessageModel ask)
    {
        int nowTokens = GPT3Tokenizer.Encode(ask.Message).Count;
        List<Message> msgs = new()
        {
            new() { role = "user", content = ask.Message }
        };

        for (int i = messages.Count - 1; i >= 0; i--)
        {
            ChatMessageModel cm = messages[i];

            nowTokens += GPT3Tokenizer.Encode(cm.Message).Count;

            if (nowTokens > Chat35Resource.MaxTokens) break;

            msgs.Add(new()
            {
                role = cm.User == ChatRole.User ? "user" : "assistant",
                content = cm.Message
            });
        }
        msgs.Add(new() { role = "system", content = Chat35Resource.SystemMessage });

        msgs.Reverse();

        return msgs;
    }

    private List<Message> GetMessagesWithOutLog(string ask)
    {
        List<Message> messages = new()
        {
            new() { role = "system", content = Chat35Resource.SystemMessage },
            new() { role = "user", content = ask }
        };

        return messages;
    }
}