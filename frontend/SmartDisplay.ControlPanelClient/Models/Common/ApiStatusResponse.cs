using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Common;

public class ApiStatusResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    public bool IsSuccess => Status.Equals("success", StringComparison.OrdinalIgnoreCase);
}
