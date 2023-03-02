using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SyNemo.OpenAI.Comm
{
    internal static class HttpHelper
    {
        private const string url = "https://api.openai.com/v1/completions";
        private const string urlChat = "https://api.openai.com/v1/chat/completions";
        private static HttpClient _client;

        internal static void Init(string key)
        {
            _client = new();
            _client.DefaultRequestHeaders.Authorization = new("Bearer", key);
        }

        public static async Task<string> Post(object obj)
        {
            try
            {
                HttpContent content = GetJsonContent(JsonConvert.SerializeObject(obj));

                return await (await _client.PostAsync(url, content)).Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                throw new OpenAIException(ErrorCode.NetFail, "网络异常", e);
            }
        }

        public static async Task<string> PostChat(object obj)
        {
            try
            {
                HttpContent content = GetJsonContent(JsonConvert.SerializeObject(obj));

                return await (await _client.PostAsync(urlChat, content)).Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                throw new OpenAIException(ErrorCode.NetFail, "网络异常", e);
            }
        }

        private static HttpContent GetJsonContent(string json)
        {
            StringContent content = new(json);
            content.Headers.ContentType = new("application/json");
            return content;
        }
    }
}
