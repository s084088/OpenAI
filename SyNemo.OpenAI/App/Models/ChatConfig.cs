namespace SyNemo.OpenAI.Models
{
    /// <summary>
    /// 聊天配置
    /// </summary>
    public class ChatConfig
    {
        /// <summary>
        /// 回答多样性，取值范围为 0.0 ~ 1.0，默认值为0.5。
        /// </summary>
        public float Temperature { get; set; } = 0.5f;

        /// <summary>
        /// 质量好的前%多，取值范围为 0.0 ~ 1.0，默认值为 1。
        /// </summary>
        public float Top_p { get; set; } = 1f;

        /// <summary>
        /// 用户名字， 默认为“Nemo”
        /// </summary>
        public string UserName { get; set; } = "Nemo";
    }
}
