using System;

namespace SyNemo.OpenAI.Models
{
    /// <summary>
    /// 聊天模型
    /// </summary>
    public class ChatMessageModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public ChatRole User { get; internal set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// 本次应该消耗的tokens
        /// </summary>
        public int Tokens { get; internal set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime Time { get; internal set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        internal ChatMessageModel(ChatRole user, string message)
        {
            User = user;
            Message = message.Trim();
            Time = DateTime.Now;
        }

        /// <summary>
        /// 获取对话文本
        /// </summary>
        /// <returns></returns>
        internal string GetMessage() => User.GetRoleName() + Message + ChatResource.NewLine;
    }
}
