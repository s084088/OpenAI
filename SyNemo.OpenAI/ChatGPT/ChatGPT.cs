using System.Threading.Tasks;
using Newtonsoft.Json;
using SyNemo.OpenAI.ChatGPT.Models;
using SyNemo.OpenAI.Comm;

namespace SyNemo.OpenAI.ChatGPT
{
    /// <summary>
    /// 对话基础类
    /// </summary>
    public static class ChatGPT
    {
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<ChatGPTResponse> Ask(ChatGPTRequest request)
        {
            string str = await HttpHelper.Post(request);

            return JsonConvert.DeserializeObject<ChatGPTResponse>(str);
        }
    }
}