using SmartDisplay.ControlPanelClient.Services.Common;
using SmartDisplay.Shared.Models.Screens;

namespace SmartDisplay.ControlPanelClient.Services.Screens;

public class ApiScreensService : IScreensService
{
    private readonly AuthorizedApiClient _apiClient;

    public ApiScreensService(AuthorizedApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<List<ScreenDto>> GetScreensAsync()
    {
        try
        {
            return await _apiClient.GetAsync<List<ScreenDto>>("api/screens") ?? new List<ScreenDto>();
        }
        catch
        {
            return FallbackData.CreateScreens();
        }
    }

    public async Task<CreateScreenResponse> SaveScreenAsync(CreateScreenRequest request)
    {
        try
        {
            var screen = await _apiClient.PostAsync<ScreenDto>("api/screens", request);

            return screen is null
                ? new CreateScreenResponse { Status = ApiError.Status, ScreenId = request.Id }
                : new CreateScreenResponse { Status = "success", ScreenId = screen.Id };
        }
        catch
        {
            return new CreateScreenResponse { Status = ApiError.Status, ScreenId = request.Id };
        }
    }

    public async Task<AssignScreensResponse> AssignTemplateAsync(AssignScreensRequest request)
    {
        try
        {
            // Actual API returns a string, not an object with status/assigned_screens.
            await _apiClient.PostAsync<string>("api/screens/assign", request);
            return new AssignScreensResponse
            {
                Status = "success",
                AssignedScreens = request.ScreenIds.ToList()
            };
        }
        catch
        {
            return new AssignScreensResponse { Status = ApiError.Status, AssignedScreens = new List<string>() };
        }
    }
}
