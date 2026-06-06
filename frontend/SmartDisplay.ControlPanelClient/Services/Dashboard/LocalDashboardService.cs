using SmartDisplay.ControlPanelClient.Models.Dashboard;
using SmartDisplay.ControlPanelClient.Services.Common;
using SmartDisplay.ControlPanelClient.Services.Emergency;
using SmartDisplay.ControlPanelClient.Services.Screens;
using SmartDisplay.ControlPanelClient.Services.Templates;

namespace SmartDisplay.ControlPanelClient.Services.Dashboard;

public class LocalDashboardService : IDashboardService
{
    private readonly IScreensService _screensService;
    private readonly ITemplatesService _templatesService;
    private readonly IEmergencyService _emergencyService;

    public LocalDashboardService(
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

    public Task<List<DashboardRecentEventDto>> GetRecentEventsAsync()
    {
        return Task.FromResult(FallbackData.CreateDashboardRecentEvents());
    }
}
