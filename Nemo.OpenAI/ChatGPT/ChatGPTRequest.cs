using System.Collections.Generic;

namespace SyNemo.OpenAI.ChatGPT
{
    /// <summary>
    /// ChatGPT请求入参
    /// </summary>
    internal class ChatGPTRequest
    {
        /// <summary>
        /// 对话主体
        /// </summary>
        public string prompt { get; set; }

        /// <summary>
        /// 返回最大字节数 不超过4000
        /// </summary>
        public string max_tokens { get; set; }

        /// <summary>
        /// 终止生成的标识，可以是字符串或字符串数组。可以理解
        /// </summary>
        public List<string> stop { get; set; }

        /// <summary>
        /// 后缀,一般不填
        /// </summary>
        public string suffix { get; set; }

        /// <summary>
        /// 回答随机性，生成文本的多样性，取值范围为 0.0 ~ 1.0。和top_p二者选一
        /// </summary>
        public float temperature { get; set; }

        /// <summary>
        /// 质量好的前%多少, 取值范围为 0.0 ~ 1.0, 和temperature二者选一
        /// </summary>
        public float top_p { get; set; }

        /// <summary>
        /// 生成文本的数量。
        /// </summary>
        public int n { get; set; }

        /// <summary>
        /// 对数,默认为空
        /// </summary>
        public string logprobs { get; set; }
    }
}