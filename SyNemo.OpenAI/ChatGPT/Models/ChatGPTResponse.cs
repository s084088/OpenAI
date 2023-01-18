namespace SyNemo.OpenAI.ChatGPT.Models
{
    /// <summary>
    /// ChatGPT接口返回数据
    /// </summary>
    public class ChatGPTResponse
    {
        public string id { get; set; }

        public string _object { get; set; }

        public int created { get; set; }
        public string model { get; set; }
        public Choice[] choices { get; set; }
        public Usage usage { get; set; }
        public Error error { get; set; }
    }

    /// <summary>
    /// 使用情况
    /// </summary>
    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }

    /// <summary>
    /// 对话信息
    /// </summary>
    public class Choice
    {
        public string text { get; set; }
        public int index { get; set; }
        public object logprobs { get; set; }
        public string finish_reason { get; set; }
    }

    /// <summary>
    /// 错误信息
    /// </summary>
    public class Error
    {
        public string message { get; set; }
        public string type { get; set; }
        public object param { get; set; }
        public string code { get; set; }
    }

}