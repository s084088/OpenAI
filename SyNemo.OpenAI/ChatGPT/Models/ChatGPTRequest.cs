using System.Collections.Generic;

namespace SyNemo.OpenAI.ChatGPT.Models
{
    /// <summary>
    /// ChatGPT请求入参
    /// </summary>
    public class ChatGPTRequest
    {
        /// <summary>
        /// 模型
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// 对话主体
        /// </summary>
        public string prompt { get; set; }

        /// <summary>
        /// 返回最大字节数 不超过4000
        /// </summary>
        public int max_tokens { get; set; }

        /// <summary>
        /// 终止生成的标识，可以是字符串或字符串数组。可以理解
        /// </summary>
        public string[] stop { get; set; }

        /// <summary>
        /// 回答随机性，生成文本的多样性，取值范围为 0.0 ~ 1.0。和top_p二者选一
        /// </summary>
        public float temperature { get; set; } = 0.5f;

        /// <summary>
        /// 质量好的前%多少, 取值范围为 0.0 ~ 1.0, 和temperature二者选一
        /// </summary>
        public float top_p { get; set; } = 1f;
    }
}