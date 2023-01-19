﻿using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SyNemo.OpenAI.Comm
{
    internal static class HttpHelper
    {
        private const string url = "https://api.openai.com/v1/completions";
        private static HttpClient _client;

        internal static void Init(string key)
        {
            _client = new();
            _client.DefaultRequestHeaders.Authorization = new("Bearer", key);
        }

        public static async Task<string> Post(object obj)
        {
            HttpContent content = GetJsonContent(JsonConvert.SerializeObject(obj));

            return await (await _client.PostAsync(url, content)).Content.ReadAsStringAsync();
        }

        private static HttpContent GetJsonContent(string json)
        {
            StringContent content = new(json);
            content.Headers.ContentType = new("application/json");
            return content;
        }
    }
}