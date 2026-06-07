using System.Text.Json.Serialization;

namespace SmartDisplay.Shared.Models.Ujin;

public class StorageFreeResponse
{
    [JsonPropertyName("total_free")]
    public int TotalFree { get; set; }
}
