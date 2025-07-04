using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ChatGPTIntegration.Data;
using Microsoft.Extensions.Logging;

namespace ChatGPTIntegration.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly AppDbContext _dbContext;
        private readonly ILogger<OpenAIService> _logger;

        public OpenAIService(HttpClient httpClient, AppDbContext dbContext, ILogger<OpenAIService> logger)
        {
            _httpClient = httpClient;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<string> CallOpenAI(string prompt)
        {
            var apiKey = _dbContext.Settings.FirstOrDefault(s => s.Key == "OpenAI_API_Key")?.Value;

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Add("Authorization", $"Bearer {apiKey}");

            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = prompt }
            }
            ,
                functions = new[]
                  {
            new {
                name = "getCustomers",
                description = "Get the list of customers",
                   parameters = new { type = "object", properties = new { } }
                }
          }
            };

            request.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            _logger.LogInformation("Calling OpenAI API...");
            var response = await _httpClient.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();

            return responseContent;
        }
    }
}
