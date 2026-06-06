using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Templates;

public class TemplateConfigDto
{
    [JsonPropertyName("layout")]
    public string Layout { get; set; } = "grid";

    [JsonPropertyName("widgets")]
    public List<string> Widgets { get; set; } = new();

    [JsonPropertyName("theme")]
    public string Theme { get; set; } = "light";
}
