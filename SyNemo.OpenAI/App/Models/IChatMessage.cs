namespace SyNemo.OpenAI.Models
{
    /// <summary>
    /// 聊天信息接口
    /// </summary>
    public interface IChatMessage
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public ChatRole User { get; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; }
    }

    /// <summary>
    /// 聊天角色
    /// </summary>
    public enum ChatRole
    {
        /// <summary>
        /// 用户
        /// </summary>
        User = 1,

        /// <summary>
        /// AI
        /// </summary>
        AI = 2
    }
}