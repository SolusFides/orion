using System.Text.Json.Serialization;

namespace SmartDisplay.Shared.Models.Emergency;

public class EmergencyActivateRequest
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("screen_ids")]
    public List<string> ScreenIds { get; set; } = new();

    [JsonPropertyName("priority")]
    public int Priority { get; set; } = 1;

    [JsonPropertyName("timeout_minutes")]
    public int TimeoutMinutes { get; set; } = 30;
}
