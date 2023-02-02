using System.Collections.Generic;
using System.Threading.Tasks;
using SyNemo.OpenAI.App.Models;
using SyNemo.OpenAI.ChatGPT.Models;
using SyNemo.OpenAI.Models;

namespace SyNemo.OpenAI
{
    /// <summary>
    /// 单个聊天对象
    /// </summary>
    public class Chat
    {
        private readonly ChatConfig _config;
        private readonly List<ChatMessageModel> messages = new();

        /// <summary>
        /// 聊天记录
        /// </summary>
        public IEnumerable<ChatMessageModel> Messages => messages.AsReadOnly();

        /// <summary>
        /// 创建聊天
        /// </summary>
        /// <param name="config">聊天配置</param>
        public Chat(ChatConfig config = null)
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
            string _prompt = GetPrompt(userMessage);
            ChatGPTRequest request = GetRequest(_prompt);
            ChatGPTResponse response = await ChatGPT.ChatGPT.Ask(request);
            ChatMessageModel aiMessage = new(ChatRole.AI, response.choices[0].text.Trim());

            userMessage.Tokens = response.usage.prompt_tokens;
            aiMessage.Tokens = response.usage.completion_tokens;

            messages.Add(userMessage);
            messages.Add(aiMessage);

            model.Prompt = _prompt;
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
            ChatGPTRequest request = GetRequest(model.Question);
            ChatGPTResponse response = await ChatGPT.ChatGPT.Ask(request);

            model.Prompt = model.Question;
            model.Answer = response.choices[0].text.Trim();
            model.SendToken = response.usage.prompt_tokens;
            model.ReceiveToken = response.usage.completion_tokens;

            return model.Answer;
        }

        /// <summary>
        /// 构建入参
        /// </summary>
        /// <param name="_prompt"></param>
        /// <returns></returns>
        private ChatGPTRequest GetRequest(string _prompt) => new()
        {
            model = ChatResource.Model,
            prompt = _prompt,
            temperature = _config.Temperature,
            top_p = _config.Top_p,

            max_tokens = ChatResource.MaxTokens - _prompt.Length * 2,
            stop = new string[] { ChatResource.UserName, ChatResource.AiName }
        };

        /// <summary>
        /// 获取新对话的Prompt
        /// </summary>
        /// <param name="ask"></param>
        /// <returns></returns>
        private string GetPrompt(ChatMessageModel ask)
        {
            string prompt = ask.GetMessage() + ChatResource.AiName;

            for (int i = messages.Count - 1; i >= 0; i--)
            {
                ChatMessageModel cm = messages[i];
                string str = cm.GetMessage(); ;
                if (str.Length + prompt.Length > ChatResource.MaxTokens / 4) break;
                prompt = str + prompt;
            }

            return ChatResource.Title + prompt;
        }
    }
}