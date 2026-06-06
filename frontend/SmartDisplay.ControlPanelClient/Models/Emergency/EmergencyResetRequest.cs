using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Emergency;

public class EmergencyResetRequest
{
    [JsonPropertyName("screen_ids")]
    public List<string> ScreenIds { get; set; } = new();
}
