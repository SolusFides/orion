using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Templates;

public class DeleteTemplateResponse
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("deleted_id")]
    public string DeletedId { get; set; } = string.Empty;

    public bool IsSuccess => Status.Equals("success", StringComparison.OrdinalIgnoreCase);
}
