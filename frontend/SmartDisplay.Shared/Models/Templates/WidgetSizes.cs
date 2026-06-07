namespace SmartDisplay.Shared.Models.Templates;

public static class WidgetSizes
{
    public const string Small = "small";
    public const string Normal = "normal";
    public const string Wide = "wide";
    public const string Tall = "tall";
    public const string Large = "large";
    public const string Full = "full";

    public static IReadOnlyList<string> All { get; } = new[]
    {
        Small,
        Normal,
        Wide,
        Tall,
        Large,
        Full
    };

    public static string GetDisplayName(string size)
    {
        return size switch
        {
            Small => "Маленький",
            Normal => "Обычный",
            Wide => "Широкий",
            Tall => "Высокий",
            Large => "Большой",
            Full => "На всю ширину",
            _ => "Обычный"
        };
    }

    public static WidgetGridSpan GetDefaultSpan(string size, int columns)
    {
        var safeColumns = Math.Clamp(columns, 1, 12);

        return size switch
        {
            Small => new WidgetGridSpan(1, 1),
            Normal => new WidgetGridSpan(Math.Min(2, safeColumns), 1),
            Wide => new WidgetGridSpan(safeColumns, 1),
            Tall => new WidgetGridSpan(Math.Min(2, safeColumns), 2),
            Large => new WidgetGridSpan(safeColumns, 2),
            Full => new WidgetGridSpan(safeColumns, 3),
            _ => new WidgetGridSpan(Math.Min(2, safeColumns), 1)
        };
    }
}
