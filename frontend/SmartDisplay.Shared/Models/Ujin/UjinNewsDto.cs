using System.Text.Json.Serialization;

namespace SmartDisplay.Shared.Models.Ujin;

public class UjinNewsDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("images")]
    public List<string> Images { get; set; } = new();
}
