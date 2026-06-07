using System.Text.Json;
using System.Net.Http.Json;
using SmartDisplay.Shared.Models.ClientDisplay;
using SmartDisplay.Shared.Models.Templates;

namespace SmartDisplay.ScreenClient.Services;

public class ApiClientScreenService : IClientScreenService
{
    private readonly HttpClient _httpClient;

    public ApiClientScreenService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ClientScreenResponse> GetScreenAsync(string screenId)
    {
        try
        {
            using var response = await _httpClient.GetAsync($"api/client/screen/{Uri.EscapeDataString(screenId)}");
            if (!response.IsSuccessStatusCode)
            {
                return ScreenFallbackData.CreateScreen(screenId, screenId.Contains("emergency", StringComparison.OrdinalIgnoreCase));
            }

            var content = await response.Content.ReadAsStringAsync();
            var parsed = DeserializeScreenResponse(content);
            return parsed ?? ScreenFallbackData.CreateScreen(screenId, screenId.Contains("emergency", StringComparison.OrdinalIgnoreCase));
        }
        catch
        {
            return ScreenFallbackData.CreateScreen(screenId, screenId.Contains("emergency", StringComparison.OrdinalIgnoreCase));
        }
    }

    public async Task<ClientScreenResponse> GetPreviewAsync(string templateId)
    {
        try
        {
            var templates = await _httpClient.GetFromJsonAsync<List<TemplateDto>>("api/templates", ScreenApiJson.Options) ?? new List<TemplateDto>();
            var template = templates.FirstOrDefault(item => item.Id == templateId)
                ?? ScreenFallbackData.CreateTemplate(templateId, "Предпросмотр шаблона");

            return new ClientScreenResponse
            {
                ScreenId = "preview-screen",
                Template = template,
                ActiveEmergency = null
            };
        }
        catch
        {
            return new ClientScreenResponse
            {
                ScreenId = "preview-screen",
                Template = ScreenFallbackData.CreateTemplate(templateId, "Предпросмотр шаблона"),
                ActiveEmergency = null
            };
        }
    }

    private static ClientScreenResponse? DeserializeScreenResponse(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return null;
        }

        var trimmed = content.Trim();

        try
        {
            if (trimmed.StartsWith('"'))
            {
                var innerJson = JsonSerializer.Deserialize<string>(trimmed, ScreenApiJson.Options);
                if (string.IsNullOrWhiteSpace(innerJson) || !innerJson.TrimStart().StartsWith('{'))
                {
                    return null;
                }

                return JsonSerializer.Deserialize<ClientScreenResponse>(innerJson, ScreenApiJson.Options);
            }

            return JsonSerializer.Deserialize<ClientScreenResponse>(trimmed, ScreenApiJson.Options);
        }
        catch
        {
            return null;
        }
    }
}
