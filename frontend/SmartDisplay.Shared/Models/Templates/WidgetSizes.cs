namespace SmartDisplay.Shared.Models.Templates;

public static class WidgetSizes
{
    public const string Small = "small";
    public const string Normal = "normal";
    public const string Wide = "wide";
    public const string Tall = "tall";
    public const string Large = "large";

    public static IReadOnlyList<string> All { get; } = new[]
    {
        Small,
        Normal,
        Wide,
        Tall,
        Large
    };

    public static string GetDisplayName(string size)
    {
        return size switch
        {
            Small => "Малый",
            Normal => "Обычный",
            Wide => "Широкий",
            Tall => "Высокий",
            Large => "Крупный",
            _ => "Обычный"
        };
    }
}
