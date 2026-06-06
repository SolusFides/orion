using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Emergency;

public class EmergencyActivateResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("emergency_id")]
    public string EmergencyId { get; set; } = string.Empty;

    public bool IsSuccess => Status.Equals("success", StringComparison.OrdinalIgnoreCase);
}
