using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Templates;

public class UpdateTemplateRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("config")]
    public TemplateConfigDto Config { get; set; } = new();
}
