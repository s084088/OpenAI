using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SyNemo.OpenAI.ChatGPT.Models;
using SyNemo.OpenAI.Models;

namespace SyNemo.OpenAI
{
    /// <summary>
    /// 单个聊天对象
    /// </summary>
    public class Chat
    {
        private const string model = "text-davinci-003";
        private const int maxTokens = 4096;

        private const string title = "以下是人类和AI的对话";
        private const string ai = "AI";
        private const string colon = "： ";
        private const string newLine = "\n";

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
            {
                foreach (IChatMessage message in _config.Messages)
                    AddChat(messages, message.User, message.Message);
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="ask">你当次讲的话</param>
        /// <returns></returns>
        public async Task<string> Ask(string ask)
        {
            ChatGPTRequest request = GetRequest(ask);

            ChatGPTResponse response = await ChatGPT.ChatGPT.Ask(request);

            CheckResponse(response);

            string asw = response.choices[0].text.Trim();

            AddChat(messages, ChatRole.User, ask, response.usage.prompt_tokens);
            AddChat(messages, ChatRole.AI, asw, response.usage.completion_tokens);
            return asw;
        }

        /// <summary>
        /// 返回结果检查
        /// </summary>
        /// <param name="response"></param>
        private void CheckResponse(ChatGPTResponse response)
        {
            if (response.error != null)
            {
                if (response.error.code == "invalid_api_key")
                    throw new("Key无效");

                throw new(response.error.message);
            }
        }

        /// <summary>
        /// 构建入参
        /// </summary>
        /// <param name="ask"></param>
        /// <returns></returns>
        private ChatGPTRequest GetRequest(string ask)
        {
            ask = ask.Trim();
            List<ChatMessageModel> chats = Messages.ToList();
            AddChat(chats, ChatRole.User, ask);

            string p = GetPrompt(chats);

            return new()
            {
                model = model,
                prompt = p,
                temperature = _config.Temperature,
                top_p = _config.Top_p,

                max_tokens = maxTokens - p.Length * 2,
                stop = new string[] { ai, _config.UserName }
            };
        }

        /// <summary>
        /// 添加对话
        /// </summary>
        /// <param name="chats"></param>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        private void AddChat(List<ChatMessageModel> chats, ChatRole user, string message, int tokens = 0)
        {
            chats.Add(new() { User = user, Message = message, tokens = tokens });
        }

        /// <summary>
        /// 获取聊天提示
        /// </summary>
        /// <returns></returns>
        private string GetPrompt(List<ChatMessageModel> chats)
        {
            string prompt = ai + colon;

            for (int i = chats.Count - 1; i >= 0; i--)
            {
                ChatMessageModel cm = chats[i];
                string str = GetRoleName(cm.User) + colon + cm.Message + newLine;
                if (str.Length + prompt.Length > maxTokens / 4) break;
                prompt = str + prompt;
            }

            prompt = title + newLine + prompt;

            return prompt;
        }

        /// <summary>
        /// 获取用户名称
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        private string GetRoleName(ChatRole role) => role switch
        {
            ChatRole.AI => ai,
            ChatRole.User => _config.UserName,
            _ => null,
        };
    }
}