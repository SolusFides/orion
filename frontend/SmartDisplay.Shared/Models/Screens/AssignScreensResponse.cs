using System.Text.Json.Serialization;

namespace SmartDisplay.Shared.Models.Screens;

public class AssignScreensResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("assigned_screens")]
    public List<string> AssignedScreens { get; set; } = new();

    public bool IsSuccess => Status.Equals("success", StringComparison.OrdinalIgnoreCase);
}
