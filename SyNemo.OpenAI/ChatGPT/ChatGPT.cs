using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SyNemo.OpenAI.ChatGPT.Models;
using SyNemo.OpenAI.Comm;

namespace SyNemo.OpenAI.ChatGPT
{
    /// <summary>
    /// 对话基础类
    /// </summary>
    internal static class ChatGPT
    {
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<ChatGPTResponse> Ask(ChatGPTRequest request)
        {
            string str = await HttpHelper.Post(request);

            ChatGPTResponse response = JsonConvert.DeserializeObject<ChatGPTResponse>(str);

            if (response.error != null)
            {
                if (response.error.code == "invalid_api_key")
                    throw new OpenAIException(ErrorCode.TokenInvalid, "Key无效");

                throw new Exception(response.error.message);
            }

            return response;
        }
    }
}