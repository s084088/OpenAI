using Newtonsoft.Json;
using SyNemo.OpenAI.ChatGPT35.Models;
using SyNemo.OpenAI.Comm;
using System;
using System.Threading.Tasks;

namespace SyNemo.OpenAI.ChatGPT35;
internal class ChatGPT35
{
    /// <summary>
    /// 发送信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public static async Task<ChatResponse> Ask(ChatRequest request)
    {
        string str = await HttpHelper.PostChat(request);

        ChatResponse response = JsonConvert.DeserializeObject<ChatResponse>(str);

        if (response.error != null)
        {
            if (response?.error?.code == "invalid_api_key")
                throw new OpenAIException(ErrorCode.TokenInvalid, "Key无效");

            if (response?.error?.message?.Contains("exceeded your current quota") == true)
                throw new OpenAIException(ErrorCode.Arrears, "账户欠费");

            throw new Exception(response.error.message);
        }

        return response;
    }
}