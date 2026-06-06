using SmartDisplay.Shared.Models.Screens;

namespace SmartDisplay.ControlPanelClient.Services.Screens;

public interface IScreensService
{
    Task<List<ScreenDto>> GetScreensAsync();
    Task<CreateScreenResponse> SaveScreenAsync(CreateScreenRequest request);
    Task<AssignScreensResponse> AssignTemplateAsync(AssignScreensRequest request);
}
