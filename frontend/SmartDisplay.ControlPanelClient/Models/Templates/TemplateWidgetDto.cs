using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Templates;

public class TemplateWidgetDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = WidgetTypes.StaticText;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("order")]
    public int Order { get; set; } = 1;

    [JsonPropertyName("size")]
    public string Size { get; set; } = WidgetSizes.Normal;

    [JsonPropertyName("settings")]
    public Dictionary<string, string> Settings { get; set; } = new();

    [JsonIgnore]
    public string DisplayTitle => string.IsNullOrWhiteSpace(Title)
        ? WidgetTypes.GetDisplayName(Type)
        : Title;

    public static TemplateWidgetDto Create(string type, int order)
    {
        return new TemplateWidgetDto
        {
            Id = $"widget-{type}-{Guid.NewGuid():N}",
            Type = type,
            Title = WidgetTypes.GetDisplayName(type),
            Order = order,
            Size = WidgetSizes.Normal,
            Settings = WidgetTypes.CreateDefaultSettings(type)
        };
    }

    public TemplateWidgetDto CloneNormalized()
    {
        return new TemplateWidgetDto
        {
            Id = string.IsNullOrWhiteSpace(Id) ? $"widget-{Type}-{Guid.NewGuid():N}" : Id,
            Type = string.IsNullOrWhiteSpace(Type) ? WidgetTypes.StaticText : Type,
            Title = string.IsNullOrWhiteSpace(Title) ? WidgetTypes.GetDisplayName(Type) : Title,
            Order = Order <= 0 ? 1 : Order,
            Size = string.IsNullOrWhiteSpace(Size) ? WidgetSizes.Normal : Size,
            Settings = new Dictionary<string, string>(Settings)
        };
    }

    public override string ToString()
    {
        return Type;
    }
}
