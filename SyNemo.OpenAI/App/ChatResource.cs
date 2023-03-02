using System;
using System.Text;
using SyNemo.OpenAI.Models;

namespace SyNemo.OpenAI
{
    internal static class ChatResource
    {
        public static string NewLine = Environment.NewLine;
        public static string Model = "gpt-3.5-turbo";

        public static string AiName = "AI： ";
        public static string UserName = "Nemo： ";

        public static string Title = "以下是人类和你的对话" + NewLine + NewLine;

        public static int MaxTokens = 3700;


        /// <summary>
        /// 获取用户名称
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        internal static string GetRoleName(this ChatRole role) => role switch
        {
            ChatRole.AI => AiName,
            ChatRole.User => UserName,
            _ => null,
        };
    }
}