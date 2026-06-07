using SmartDisplay.ControlPanelClient.Models.Dashboard;

namespace SmartDisplay.ControlPanelClient.Services.Dashboard;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync();
    Task<List<DashboardQuickActionDto>> GetQuickActionsAsync();
    Task<List<DashboardRecentEventDto>> GetRecentEventsAsync();
}
