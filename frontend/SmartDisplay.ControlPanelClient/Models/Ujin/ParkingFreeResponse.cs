using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Ujin;

public class ParkingFreeResponse
{
    [JsonPropertyName("total_free")]
    public int TotalFree { get; set; }

    [JsonPropertyName("zones")]
    public List<ParkingZoneDto> Zones { get; set; } = new();
}
