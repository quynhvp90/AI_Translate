using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GPT_Translate
{
    public class OpenAIHelper
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string Endpoint = "https://api.openai.com/v1/responses";

        public OpenAIHelper(string apiKey)
        {
            _apiKey = apiKey;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        /// <summary>
        /// Gửi prompt tới ChatGPT và nhận kết quả text
        /// </summary>
        public async Task<string> SendMessageAsync(string prompt, string model = "gpt-4.1-mini")
        {
            var requestBody = new
            {
                model = model,
                input = prompt
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(Endpoint, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"OpenAI API Error: {response.StatusCode} - {responseString}");
            }

            return ExtractText(responseString);
        }

        /// <summary>
        /// Parse response JSON để lấy text output
        /// </summary>
        private string ExtractText(string json)
        {
            using var doc = JsonDocument.Parse(json);

            try
            {
                var output = doc.RootElement
                    .GetProperty("output")[0]
                    .GetProperty("content")[0]
                    .GetProperty("text")
                    .GetString();

                return output ?? string.Empty;
            }
            catch
            {
                return "Cannot parse response: " + json;
            }
        }
    }
}
