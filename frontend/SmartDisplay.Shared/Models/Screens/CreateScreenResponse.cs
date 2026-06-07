using System.Text.Json.Serialization;

namespace SmartDisplay.Shared.Models.Screens;

public class CreateScreenResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("screen_id")]
    public string ScreenId { get; set; } = string.Empty;

    public bool IsSuccess => Status.Equals("success", StringComparison.OrdinalIgnoreCase);
}
