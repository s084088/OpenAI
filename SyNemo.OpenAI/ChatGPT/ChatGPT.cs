﻿using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SyNemo.OpenAI.ChatGPT.Models;
using SyNemo.OpenAI.Comm;

namespace SyNemo.OpenAI.ChatGPT;

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
        string str = await HttpHelper.PostChat(request);

        ChatGPTResponse response = JsonConvert.DeserializeObject<ChatGPTResponse>(str);

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