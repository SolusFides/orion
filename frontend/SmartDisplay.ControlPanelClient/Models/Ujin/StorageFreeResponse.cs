using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Ujin;

public class StorageFreeResponse
{
    [JsonPropertyName("total_free")]
    public int TotalFree { get; set; }
}
