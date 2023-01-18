using System;

namespace SyNemo.OpenAI.Models
{
    /// <summary>
    /// 聊天模型
    /// </summary>
    public class ChatModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string User { get; internal set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// 消耗的tokens
        /// </summary>
        public int tokens { get; internal set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime Time { get; } = DateTime.Now;
    }
}
