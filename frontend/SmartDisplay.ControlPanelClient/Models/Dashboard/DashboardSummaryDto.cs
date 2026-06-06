namespace SmartDisplay.ControlPanelClient.Models.Dashboard;

public class DashboardSummaryDto
{
    public int ScreensTotal { get; set; }
    public int ScreensWithTemplate { get; set; }
    public int TemplatesTotal { get; set; }
    public int ActiveEmergenciesTotal { get; set; }
    public DateTime GeneratedAt { get; set; } = DateTime.Now;
}
