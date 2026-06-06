using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Screens;

public class CreateScreenRequest
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("complex_id")]
    public int ComplexId { get; set; }

    [JsonPropertyName("building_id")]
    public int BuildingId { get; set; }

    [JsonPropertyName("entrance")]
    public int Entrance { get; set; }
}
