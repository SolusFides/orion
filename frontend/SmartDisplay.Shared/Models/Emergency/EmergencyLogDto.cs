using System.Text.Json.Serialization;

namespace SmartDisplay.Shared.Models.Emergency;

public class EmergencyLogDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("action")]
    public string Action { get; set; } = string.Empty;

    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("target_screens")]
    public List<string> TargetScreens { get; set; } = new();

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
}
