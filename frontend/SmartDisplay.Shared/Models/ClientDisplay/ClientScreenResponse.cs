using System.Text.Json.Serialization;
using SmartDisplay.Shared.Models.Templates;

namespace SmartDisplay.Shared.Models.ClientDisplay;

public class ClientScreenResponse
{
    [JsonPropertyName("screen_id")]
    public string ScreenId { get; set; } = string.Empty;

    [JsonPropertyName("template")]
    public TemplateDto? Template { get; set; }

    [JsonPropertyName("active_emergency")]
    public ClientActiveEmergencyDto? ActiveEmergency { get; set; }
}
