using System.Text.Json.Serialization;

namespace SmartDisplay.Shared.Models.Templates;

public class TemplateDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("config")]
    public TemplateConfigDto Config { get; set; } = new();
}
