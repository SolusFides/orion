using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Templates;

public class TemplateConfigDto
{
    [JsonPropertyName("layout")]
    public string Layout { get; set; } = TemplateLayouts.Grid;

    [JsonPropertyName("widgets")]
    public List<string> Widgets { get; set; } = new();

    [JsonPropertyName("theme")]
    public string Theme { get; set; } = TemplateThemes.Light;

    /// <summary>
    /// Расширенная конфигурация виджетов для конструктора и предпросмотра.
    /// Старое поле widgets сохраняется для совместимости с текущим API.
    /// </summary>
    [JsonPropertyName("widget_items")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<TemplateWidgetDto>? WidgetItems { get; set; }

    [JsonIgnore]
    public bool HasExtendedWidgets => WidgetItems is { Count: > 0 };

    public List<TemplateWidgetDto> GetEffectiveWidgets()
    {
        if (WidgetItems is { Count: > 0 })
        {
            return WidgetItems
                .OrderBy(widget => widget.Order)
                .Select(widget => widget.CloneNormalized())
                .ToList();
        }

        return Widgets
            .Where(widgetType => !string.IsNullOrWhiteSpace(widgetType))
            .Select((widgetType, index) => TemplateWidgetDto.Create(widgetType, index + 1))
            .ToList();
    }

    public void SetWidgetItems(IEnumerable<TemplateWidgetDto> widgets)
    {
        var normalizedWidgets = widgets
            .Where(widget => !string.IsNullOrWhiteSpace(widget.Type))
            .OrderBy(widget => widget.Order)
            .Select((widget, index) =>
            {
                var normalized = widget.CloneNormalized();
                normalized.Order = index + 1;
                return normalized;
            })
            .ToList();

        WidgetItems = normalizedWidgets;
        Widgets = normalizedWidgets.Select(widget => widget.Type).ToList();
    }
}
