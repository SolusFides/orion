using SmartDisplay.ControlPanelClient.Models.Dashboard;
using SmartDisplay.ControlPanelClient.Models.Emergency;
using SmartDisplay.ControlPanelClient.Models.Screens;
using SmartDisplay.ControlPanelClient.Models.Templates;

namespace SmartDisplay.ControlPanelClient.Services.Common;

public static class FallbackData
{
    public static List<ScreenDto> CreateScreens()
    {
        return new List<ScreenDto>
        {
            new()
            {
                Id = "main_hall_01",
                Name = "Главный холл 1",
                ComplexId = 73,
                BuildingId = 141,
                Entrance = 1,
                CurrentTemplateId = "uuid-template-123"
            },
            new()
            {
                Id = "lift_02",
                Name = "Лифтовой холл 2",
                ComplexId = 73,
                BuildingId = 141,
                Entrance = 2,
                CurrentTemplateId = null
            },
            new()
            {
                Id = "reception_01",
                Name = "Ресепшн",
                ComplexId = 73,
                BuildingId = 141,
                Entrance = 1,
                CurrentTemplateId = "uuid-template-evening"
            }
        };
    }

    public static List<TemplateDto> CreateTemplates()
    {
        return new List<TemplateDto>
        {
            new()
            {
                Id = "uuid-template-123",
                Name = "Стандартная сетка",
                Config = new TemplateConfigDto
                {
                    Layout = "grid",
                    Widgets = new List<string> { "news", "parking" },
                    Theme = "dark"
                }
            },
            new()
            {
                Id = "uuid-template-evening",
                Name = "Вечерний информационный",
                Config = new TemplateConfigDto
                {
                    Layout = "split",
                    Widgets = new List<string> { "news", "storage" },
                    Theme = "light"
                }
            }
        };
    }

    public static List<EmergencyActiveDto> CreateActiveEmergencies()
    {
        return new List<EmergencyActiveDto>();
    }

    public static List<EmergencyLogDto> CreateEmergencyLog()
    {
        return new List<EmergencyLogDto>
        {
            new()
            {
                Id = "uuid-log-demo-1",
                Action = "activate",
                UserId = "admin",
                TargetScreens = new List<string> { "main_hall_01" },
                Text = "ВНИМАНИЕ!!! Проверка системы оповещения.",
                CreatedAt = DateTime.Now.AddMinutes(-40)
            },
            new()
            {
                Id = "uuid-log-demo-2",
                Action = "reset",
                UserId = "admin",
                TargetScreens = new List<string> { "main_hall_01" },
                Text = null,
                CreatedAt = DateTime.Now.AddMinutes(-25)
            }
        };
    }

    public static List<DashboardQuickActionDto> CreateDashboardQuickActions()
    {
        return new List<DashboardQuickActionDto>
        {
            new()
            {
                Title = "Управление экранами",
                Description = "Посмотреть список подключенных экранов и назначить шаблоны.",
                Url = "/screens"
            },
            new()
            {
                Title = "Создать шаблон",
                Description = "Собрать новый шаблон из виджетов для ЖК-дисплеев.",
                Url = "/templates/create"
            },
            new()
            {
                Title = "Режим ЧС",
                Description = "Отправить экстренное сообщение на один, несколько или все экраны.",
                Url = "/emergency"
            }
        };
    }

    public static List<DashboardRecentEventDto> CreateDashboardRecentEvents()
    {
        return new List<DashboardRecentEventDto>
        {
            new()
            {
                Title = "Шаблон назначен",
                Description = "Стандартная сетка назначена на экран Главный холл 1.",
                CreatedAt = DateTime.Now.AddMinutes(-12),
                Level = "info"
            },
            new()
            {
                Title = "Экран подключен",
                Description = "Добавлен экран Лифтовой холл 2.",
                CreatedAt = DateTime.Now.AddMinutes(-35),
                Level = "success"
            },
            new()
            {
                Title = "Проверка ЧС завершена",
                Description = "Тестовое ЧС-сообщение было сброшено.",
                CreatedAt = DateTime.Now.AddHours(-1),
                Level = "warning"
            }
        };
    }
}
