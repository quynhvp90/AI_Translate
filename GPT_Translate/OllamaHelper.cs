using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GPT_Translate
{
    public class OllamaHelper
    {
        private readonly HttpClient _http = new HttpClient();

        public OllamaHelper() { }
        public async Task<string> SendMessageAsync(string prompt)
        {
            var url = "http://localhost:11434/api/generate";

            var body = new
            {
                model = "qwen3.5", //"llama3",
                prompt = prompt,
                stream = false,
                options = new
                {
                    temperature = 0.1,   // cực thấp → tránh sáng tạo
                    top_p = 0.9,
                    top_k = 40,
                    repeat_penalty = 1.1,
                    num_ctx = 4096       // đủ cho batch lớn
                }
            };

            var json = JsonSerializer.Serialize(body);

            var response = await _http.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            var result = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(result);
            return doc.RootElement.GetProperty("response").GetString();
        }
    }
}
