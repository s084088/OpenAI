namespace SyNemo.OpenAI.ChatGPT
{
    internal class ChatGPTResponse
    {
        public string id { get; set; }
        public string _object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public Choice[] choices { get; set; }
        public Usage usage { get; set; }
        public Error error { get; set; }
    }

    internal class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }

    internal class Choice
    {
        public string text { get; set; }
        public int index { get; set; }
        public object logprobs { get; set; }
        public string finish_reason { get; set; }
    }

    public class Error
    {
        public string message { get; set; }
        public string type { get; set; }
        public object param { get; set; }
        public string code { get; set; }
    }

}