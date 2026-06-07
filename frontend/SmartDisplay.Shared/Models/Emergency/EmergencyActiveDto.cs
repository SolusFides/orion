using System.Text.Json.Serialization;

namespace SmartDisplay.Shared.Models.Emergency;

public class EmergencyActiveDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("target_screens")]
    public List<string> TargetScreens { get; set; } = new();

    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    [JsonPropertyName("timeout_minutes")]
    public int TimeoutMinutes { get; set; }

    [JsonPropertyName("activated_by")]
    public string ActivatedBy { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("resolved_at")]
    public DateTime? ResolvedAt { get; set; }
}
