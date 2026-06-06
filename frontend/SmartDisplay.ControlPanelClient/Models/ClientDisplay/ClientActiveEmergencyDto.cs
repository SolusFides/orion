using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.ClientDisplay;

public class ClientActiveEmergencyDto
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("priority")]
    public int Priority { get; set; }
}
