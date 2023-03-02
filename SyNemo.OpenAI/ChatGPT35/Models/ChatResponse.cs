namespace SyNemo.OpenAI.ChatGPT35.Models;

public class ChatResponse
{
    public string id { get; set; }
    public string _object { get; set; }
    public int created { get; set; }
    public string model { get; set; }
    public Usage usage { get; set; }
    public Choice[] choices { get; set; }

    public Error error { get; set; }
}

public class Usage
{
    public int prompt_tokens { get; set; }
    public int completion_tokens { get; set; }
    public int total_tokens { get; set; }
}

public class Choice
{
    public Message message { get; set; }
    public string finish_reason { get; set; }
    public int index { get; set; }
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