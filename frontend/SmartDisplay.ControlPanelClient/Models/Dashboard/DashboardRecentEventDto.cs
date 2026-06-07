namespace SmartDisplay.ControlPanelClient.Models.Dashboard;

public class DashboardRecentEventDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Level { get; set; } = "info";
}
