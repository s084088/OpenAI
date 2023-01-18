using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SyNemo.OpenAI.ChatGPT;
using Newtonsoft.Json;

namespace SyNemo.OpenAI
{
    /// <summary>
    /// 单个聊天对象
    /// </summary>
    public class Chat
    {
        private const string title = "以下是人类和AI的对话";
        private const string ai = "AI";
        private const string user = "Nemo";
        private const string colon = "： ";
        private const string newLine = "\n";
        private const string url = "https://api.openai.com/v1/completions";

        private readonly HttpClient _client;
        private readonly float _temperature;
        private readonly float _top_p;

        /// <summary>
        /// 聊天记录
        /// </summary>
        public List<ChatModel> Messages { get; } = new List<ChatModel>();

        /// <summary>
        /// 创建聊天
        /// </summary>
        /// <param name="top_p">质量好的前%多，取值范围为 0.0 ~ 1.0，默认值为 1。</param>
        /// <param name="temperature">回答多样性，取值范围为 0.0 ~ 1.0，默认值为0.5。</param>
        /// <remarks>内置两种参数建议只调节其中一个</remarks>
        public Chat(float temperature = 1f, float top_p = 0.5f)
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OpenAI.Key);

            _temperature = temperature;
            _top_p = top_p;
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="ask">你当次讲的话</param>
        /// <returns></returns>
        public async Task<string> Ask(string ask)
        {
            ask = ask.Trim();
            List<ChatModel> chats = Messages.ToArray().ToList();
            AddChat(chats, user, ask);

            string p = GetPrompt(chats);
            HttpContent content = GetJsonContent(JsonConvert.SerializeObject(new
            {
                model = "text-davinci-003",
                prompt = p,
                temperature = _temperature,
                top_p = _top_p,

                max_tokens = 4096 - p.Length * 2,
                stop = new string[] { ai, user }
            }));

            string str = await (await _client.PostAsync(url, content)).Content.ReadAsStringAsync();

            ChatGPTResponse response = JsonConvert.DeserializeObject<ChatGPTResponse>(str);

            if (response.error != null)
            {
                if (response.error.code == "invalid_api_key")
                    throw new System.Exception("Key无效");

                throw new System.Exception(response.error.message);
            }

            string asw = response.choices[0].text.Trim();

            AddChat(Messages, user, ask);
            AddChat(Messages, ai, asw);
            return asw;
        }

        /// <summary>
        /// 添加对话
        /// </summary>
        /// <param name="chats"></param>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        internal void AddChat(List<ChatModel> chats, string user, string message)
        {
            chats.Add(new ChatModel() { User = user, Message = message });
        }

        /// <summary>
        /// 获取聊天提示
        /// </summary>
        /// <returns></returns>
        private string GetPrompt(List<ChatModel> chats)
        {
            string prompt = ai + colon;

            for (int i = chats.Count - 1; i >= 0; i--)
            {
                ChatModel cm = chats[i];
                string str = cm.User + colon + cm.Message + newLine;
                if (str.Length + prompt.Length > 1000) break;
                prompt = str + prompt;
            }

            prompt = title + newLine + prompt;

            return prompt;
        }

        /// <summary>
        /// 获取http内容
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private HttpContent GetJsonContent(string json)
        {
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}