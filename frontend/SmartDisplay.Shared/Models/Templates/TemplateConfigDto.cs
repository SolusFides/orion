using System.Text.Json.Serialization;

namespace SmartDisplay.Shared.Models.Templates;

public class TemplateConfigDto
{
    [JsonPropertyName("layout")]
    public string Layout { get; set; } = TemplateLayouts.Grid;

    [JsonPropertyName("widgets")]
    public List<string> Widgets { get; set; } = new();

    [JsonPropertyName("theme")]
    public string Theme { get; set; } = TemplateThemes.Light;

    [JsonPropertyName("columns")]
    public int Columns { get; set; } = 4;

    [JsonPropertyName("gap")]
    public int Gap { get; set; } = 16;

    [JsonPropertyName("widget_items")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TemplateWidgetDto>? WidgetItems { get; set; }

    [JsonIgnore]
    public bool HasExtendedWidgets => WidgetItems is { Count: > 0 };

    [JsonIgnore]
    public int SafeColumns => Math.Clamp(Columns, 1, 8);

    [JsonIgnore]
    public int SafeGap => Math.Clamp(Gap, 0, 48);

    public List<TemplateWidgetDto> GetEffectiveWidgets()
    {
        if (WidgetItems is { Count: > 0 })
        {
            return WidgetItems
                .OrderBy(widget => widget.Order)
                .Select(widget => widget.CloneNormalized(SafeColumns))
                .ToList();
        }

        return Widgets
            .Where(widgetType => !string.IsNullOrWhiteSpace(widgetType))
            .Select((widgetType, index) => TemplateWidgetDto.Create(widgetType, index + 1, SafeColumns))
            .ToList();
    }

    public void SetWidgetItems(IEnumerable<TemplateWidgetDto> widgets)
    {
        var normalizedWidgets = widgets
            .Where(widget => !string.IsNullOrWhiteSpace(widget.Type))
            .OrderBy(widget => widget.Order)
            .Select((widget, index) =>
            {
                var normalized = widget.CloneNormalized(SafeColumns);
                normalized.Order = index + 1;
                return normalized;
            })
            .ToList();

        WidgetItems = normalizedWidgets;
        Widgets = normalizedWidgets.Select(widget => widget.Type).ToList();
    }
}
