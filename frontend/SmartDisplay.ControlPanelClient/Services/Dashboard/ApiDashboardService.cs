using SmartDisplay.ControlPanelClient.Models.Dashboard;
using SmartDisplay.ControlPanelClient.Services.Common;
using SmartDisplay.ControlPanelClient.Services.Emergency;
using SmartDisplay.ControlPanelClient.Services.Screens;
using SmartDisplay.ControlPanelClient.Services.Templates;

namespace SmartDisplay.ControlPanelClient.Services.Dashboard;

public class ApiDashboardService : IDashboardService
{
    private readonly IScreensService _screensService;
    private readonly ITemplatesService _templatesService;
    private readonly IEmergencyService _emergencyService;

    public ApiDashboardService(
        IScreensService screensService,
        ITemplatesService templatesService,
        IEmergencyService emergencyService)
    {
        _screensService = screensService;
        _templatesService = templatesService;
        _emergencyService = emergencyService;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var screens = await _screensService.GetScreensAsync();
        var templates = await _templatesService.GetTemplatesAsync();
        var activeEmergencies = await _emergencyService.GetActiveAsync();

        return new DashboardSummaryDto
        {
            ScreensTotal = screens.Count,
            ScreensWithTemplate = screens.Count(screen => screen.HasTemplate),
            TemplatesTotal = templates.Count,
            ActiveEmergenciesTotal = activeEmergencies.Count,
            GeneratedAt = DateTime.Now
        };
    }

    public Task<List<DashboardQuickActionDto>> GetQuickActionsAsync()
    {
        return Task.FromResult(FallbackData.CreateDashboardQuickActions());
    }

    public async Task<List<DashboardRecentEventDto>> GetRecentEventsAsync()
    {
        var log = await _emergencyService.GetLogAsync();

        if (log.Count == 0)
        {
            return FallbackData.CreateDashboardRecentEvents();
        }

        return log.Take(6)
            .Select(item => new DashboardRecentEventDto
            {
                Title = item.Action.Equals("activate", StringComparison.OrdinalIgnoreCase)
                    ? "Активирован режим ЧС"
                    : "Сброшен режим ЧС",
                Description = string.Join(", ", item.TargetScreens),
                CreatedAt = item.CreatedAt,
                Level = item.Action.Equals("activate", StringComparison.OrdinalIgnoreCase) ? "warning" : "info"
            })
            .ToList();
    }
}
