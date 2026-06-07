using System.Text.Json.Serialization;

namespace SmartDisplay.Shared.Models.Templates;

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

    [JsonPropertyName("col_span")]
    public int ColSpan { get; set; }

    [JsonPropertyName("row_span")]
    public int RowSpan { get; set; }

    [JsonPropertyName("settings")]
    public Dictionary<string, string> Settings { get; set; } = new();

    [JsonIgnore]
    public string DisplayTitle => string.IsNullOrWhiteSpace(Title)
        ? WidgetTypes.GetDisplayName(Type)
        : Title;

    public static TemplateWidgetDto Create(string type, int order, int columns = 4)
    {
        var span = WidgetSizes.GetDefaultSpan(WidgetSizes.Normal, columns);

        return new TemplateWidgetDto
        {
            Id = $"widget-{type}-{Guid.NewGuid():N}",
            Type = type,
            Title = WidgetTypes.GetDisplayName(type),
            Order = order,
            Size = WidgetSizes.Normal,
            ColSpan = span.ColSpan,
            RowSpan = span.RowSpan,
            Settings = WidgetTypes.CreateDefaultSettings(type)
        };
    }

    public TemplateWidgetDto CloneNormalized(int columns = 4)
    {
        var safeType = string.IsNullOrWhiteSpace(Type) ? WidgetTypes.StaticText : Type;
        var safeSize = string.IsNullOrWhiteSpace(Size) ? WidgetSizes.Normal : Size;
        var defaultSpan = WidgetSizes.GetDefaultSpan(safeSize, columns);

        return new TemplateWidgetDto
        {
            Id = string.IsNullOrWhiteSpace(Id) ? $"widget-{safeType}-{Guid.NewGuid():N}" : Id,
            Type = safeType,
            Title = string.IsNullOrWhiteSpace(Title) ? WidgetTypes.GetDisplayName(safeType) : Title,
            Order = Order <= 0 ? 1 : Order,
            Size = safeSize,
            ColSpan = ColSpan <= 0 ? defaultSpan.ColSpan : Math.Clamp(ColSpan, 1, Math.Max(columns, 1)),
            RowSpan = RowSpan <= 0 ? defaultSpan.RowSpan : Math.Clamp(RowSpan, 1, 6),
            Settings = new Dictionary<string, string>(Settings)
        };
    }

    public void ApplySizePreset(int columns)
    {
        var span = WidgetSizes.GetDefaultSpan(Size, columns);
        ColSpan = span.ColSpan;
        RowSpan = span.RowSpan;
    }

    public override string ToString()
    {
        return Type;
    }
}
