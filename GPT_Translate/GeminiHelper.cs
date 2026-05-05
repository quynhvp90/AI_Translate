using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GPT_Translate
{
    public class GeminiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        // check model uses: https://generativelanguage.googleapis.com/v1beta/models?key=AIzaSyCMCHfvFoEUSX4tXxSgViy_4QQ7cVGQ2xg
        public GeminiHelper(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<string> SendMessageAsync(string message)
        {
            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = message }
                    }
                }
            }
            };

            var json = JsonSerializer.Serialize(requestBody);

            var response = await _httpClient.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            var result = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return $"Error: {result}";

            return ExtractText(result);
        }

        private string ExtractText(string json)
        {
            using var doc = JsonDocument.Parse(json);

            try
            {
                return doc.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString() ?? "";
            }
            catch
            {
                return "Parse error: " + json;
            }
        }
    }
}
