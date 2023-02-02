namespace SyNemo.OpenAI.App.Models
{
    /// <summary>
    /// 会话模型
    /// </summary>
    public class DialogueModel
    {
        /// <summary>
        /// 发送文本
        /// </summary>
        public string Question { get; internal set; }

        /// <summary>
        /// 接收文本
        /// </summary>
        public string Answer { get; internal set; }

        /// <summary>
        /// 实际发送的文本
        /// </summary>
        public string Prompt { get; internal set; }

        /// <summary>
        /// 发送的Token数量
        /// </summary>
        public int SendToken { get; internal set; }

        /// <summary>
        /// 接收的Token数量
        /// </summary>
        public int ReceiveToken { get; internal set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="question"></param>
        public DialogueModel(string question) => Question = question;
    }
}
