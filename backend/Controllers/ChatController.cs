using Microsoft.AspNetCore.Mvc;
using ChatGPTIntegration.Services;
using Microsoft.EntityFrameworkCore;
using ChatGPTIntegration.Data;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace ChatGPTIntegration.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly OpenAIService _openAIService;
        private readonly AppDbContext _context;
        private readonly HttpClient _httpClient;
        public ChatController(AppDbContext context, OpenAIService openAIService)
        {
            _openAIService = openAIService;
            _context = context;
            _httpClient = new HttpClient();

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserPromptDto input)
        {
            var apiKey = _context.Settings.FirstOrDefault(s => s.Key == "OpenAI_API_Key")?.Value;

            //if (string.IsNullOrWhiteSpace(request.Prompt))
            //    return BadRequest("Prompt is required.");

            //var response = await _openAIService.CallOpenAI(request.Prompt);
            //return Ok(response);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            // Step 1: Call OpenAI with function definition
            var openAIRequest = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = input.Message }
                },
                functions = new[]
                {
                    new {
                        name = "getCustomers",
                        description = "Get the list of customers from the database",
                        parameters = new { type = "object", properties = new { } }
                    }
                }
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(openAIRequest), Encoding.UTF8, "application/json");

            var openAiResponse = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestContent);
            var rawResponse = await openAiResponse.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(rawResponse);

            var root = doc.RootElement;

            // Look for function call
            if (root.TryGetProperty("choices", out var choices))
            {
                var message = choices[0].GetProperty("message");

                if (message.TryGetProperty("function_call", out var functionCall))
                {
                    var functionName = functionCall.GetProperty("name").GetString();

                    if (functionName == "getCustomers")
                    {
                        var customers = await _context.Customers.ToListAsync();

                        // Call OpenAI again with function response
                        var secondRequest = new
                        {
                            model = "gpt-3.5-turbo",
                            messages = new object[]
                            {
                                new { role = "system", content = "You are a helpful assistant." },
                                new { role = "user", content = input.Message},
                                new
                                {
                                    role = "assistant",
                                    function_call = new
                                    {
                                        name = functionName,
                                        arguments = "{}"
                                    }
                                },
                                new
                                {
                                    role = "function",
                                    name = functionName,
                                    content = JsonSerializer.Serialize(customers)
                                }
                            }
                        };

                        var secondContent = new StringContent(JsonSerializer.Serialize(secondRequest), Encoding.UTF8, "application/json");
                        var secondResponse = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", secondContent);
                        var finalResult = await secondResponse.Content.ReadAsStringAsync();

                        return Ok(finalResult);
                    }
                }
            }

            return Ok(rawResponse);
        }
    }
}

public class ChatRequest
{
    public string Prompt { get; set; } = string.Empty;
}
public class UserPromptDto
{
    public string Message { get; set; }
}

