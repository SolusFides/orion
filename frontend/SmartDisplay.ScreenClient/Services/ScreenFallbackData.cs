using SmartDisplay.Shared.Models.ClientDisplay;
using SmartDisplay.Shared.Models.Templates;
using SmartDisplay.Shared.Models.Ujin;

namespace SmartDisplay.ScreenClient.Services;

public static class ScreenFallbackData
{
    public static ClientScreenResponse CreateScreen(string screenId, bool withEmergency = false)
    {
        return new ClientScreenResponse
        {
            ScreenId = screenId,
            Template = CreateTemplate("uuid-template-123", "Стандартный экран ЖК"),
            ActiveEmergency = withEmergency
                ? new ClientActiveEmergencyDto
                {
                    Text = "ВНИМАНИЕ! Проверка системы экстренного оповещения.",
                    Priority = 1
                }
                : null
        };
    }

    public static TemplateDto CreateTemplate(string id, string name)
    {
        var config = new TemplateConfigDto
        {
            Layout = TemplateLayouts.Grid,
            Theme = TemplateThemes.Dark,
            Columns = 9,
            Gap = 12
        };

        var news = TemplateWidgetDto.Create(WidgetTypes.News, 1, 9);
        news.Title = "Новости УК";
        news.Settings["limit"] = "2";

        var parking = TemplateWidgetDto.Create(WidgetTypes.Parking, 2, 9);
        parking.Title = "Свободные парковки";

        var storage = TemplateWidgetDto.Create(WidgetTypes.Storage, 3, 9);
        storage.Title = "Кладовые";

        var weather = TemplateWidgetDto.Create(WidgetTypes.Weather, 4, 9);
        weather.Title = "Погода";

        var text = TemplateWidgetDto.Create(WidgetTypes.StaticText, 5, 9);
        text.Title = "Аварийная служба";
        text.Settings["text"] = "Телефон диспетчерской: +7 342 000-00-00";
        text.Settings["subtitle"] = "Круглосуточно, заявки принимаются без выходных";

        config.SetWidgetItems(new[] { news, parking, storage, weather, text });

        return new TemplateDto
        {
            Id = id,
            Name = name,
            Config = config
        };
    }

    public static List<UjinNewsDto> CreateNews()
    {
        return new List<UjinNewsDto>
        {
            new() { Id = 1, Date = "2026-06-06", Title = "Плановая проверка систем безопасности", Text = "Работы пройдут утром, доступ в холлы сохранён." },
            new() { Id = 2, Date = "2026-06-05", Title = "Заявки через мобильное приложение", Text = "Передавайте обращения в УК без звонков и ожидания." }
        };
    }

    public static ParkingFreeResponse CreateParking()
    {
        return new ParkingFreeResponse
        {
            TotalFree = 12,
            Zones = new List<ParkingZoneDto>
            {
                new() { Name = "Наземный паркинг", FreeCount = 4 },
                new() { Name = "Подземный паркинг", FreeCount = 8 }
            }
        };
    }

    public static StorageFreeResponse CreateStorage()
    {
        return new StorageFreeResponse { TotalFree = 9 };
    }
}
