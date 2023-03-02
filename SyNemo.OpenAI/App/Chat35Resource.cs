using AI.Dev.OpenAI.GPT;

namespace SyNemo.OpenAI
{
    internal static class Chat35Resource
    {
        public static string Model = "gpt-3.5-turbo";
        public static string SystemMessage = "You are a helpful assistant.";

        private static int maxTokens = 0;

        public static int MaxTokens
        {
            get
            {
                if (maxTokens == 0)
                    maxTokens = 4000 - GPT3Tokenizer.Encode(SystemMessage).Count;
                return maxTokens;
            }
        }
    }
}