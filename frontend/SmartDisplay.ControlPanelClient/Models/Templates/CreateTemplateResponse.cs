using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Templates;

public class CreateTemplateResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("template_id")]
    public string TemplateId { get; set; } = string.Empty;

    public bool IsSuccess => Status.Equals("success", StringComparison.OrdinalIgnoreCase);
}
