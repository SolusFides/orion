using System.Text.Json.Serialization;

namespace SmartDisplay.Shared.Models.Ujin;

public class ParkingZoneDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("free_count")]
    public int FreeCount { get; set; }
}
