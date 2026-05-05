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
        public async Task<string> SendAsync(string prompt)
        {
            var url = "http://localhost:11434/api/generate";

            var body = new
            {
                model = "llama3",
                prompt = prompt,
                stream = false
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
