using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Screens;

public class AssignScreensRequest
{
    [JsonPropertyName("screen_ids")]
    public List<string> ScreenIds { get; set; } = new();

    [JsonPropertyName("template_id")]
    public string TemplateId { get; set; } = string.Empty;
}
