namespace SmartDisplay.ControlPanelClient.Models.Templates;

public static class WidgetTypes
{
    public const string News = "news";
    public const string Parking = "parking";
    public const string Storage = "storage";
    public const string StaticText = "static_text";
    public const string Weather = "weather";
    public const string Rss = "rss";
    public const string Camera = "camera";

    public static IReadOnlyList<string> All { get; } = new[]
    {
        News,
        Parking,
        Storage,
        StaticText,
        Weather,
        Rss,
        Camera
    };

    public static string GetDisplayName(string type)
    {
        return type switch
        {
            News => "Новости УК",
            Parking => "Свободные парковки",
            Storage => "Свободные кладовые",
            StaticText => "Постоянная информация",
            Weather => "Погода",
            Rss => "RSS-лента",
            Camera => "Камера",
            _ => "Виджет"
        };
    }

    public static Dictionary<string, string> CreateDefaultSettings(string type)
    {
        return type switch
        {
            News => new Dictionary<string, string>
            {
                ["building_id"] = "141",
                ["limit"] = "3"
            },
            Parking => new Dictionary<string, string>
            {
                ["building_id"] = "141"
            },
            Storage => new Dictionary<string, string>
            {
                ["building_id"] = "141"
            },
            StaticText => new Dictionary<string, string>
            {
                ["text"] = "Информация для жителей ЖК"
            },
            Weather => new Dictionary<string, string>
            {
                ["city"] = "Пермь"
            },
            Rss => new Dictionary<string, string>
            {
                ["url"] = string.Empty,
                ["limit"] = "3"
            },
            Camera => new Dictionary<string, string>
            {
                ["source"] = string.Empty
            },
            _ => new Dictionary<string, string>()
        };
    }
}
