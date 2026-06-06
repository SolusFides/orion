using SmartDisplay.ControlPanelClient.Models.Screens;
using SmartDisplay.ControlPanelClient.Services.Common;

namespace SmartDisplay.ControlPanelClient.Services.Screens;

public class MockScreensService : IScreensService
{
    private readonly List<ScreenDto> _screens = FallbackData.CreateScreens();

    public Task<List<ScreenDto>> GetScreensAsync()
    {
        return Task.FromResult(_screens.ToList());
    }

    public Task<CreateScreenResponse> SaveScreenAsync(CreateScreenRequest request)
    {
        var existing = _screens.FirstOrDefault(screen => screen.Id == request.Id);

        if (existing is null)
        {
            _screens.Add(new ScreenDto
            {
                Id = request.Id,
                Name = request.Name,
                ComplexId = request.ComplexId,
                BuildingId = request.BuildingId,
                Entrance = request.Entrance,
                CurrentTemplateId = null
            });
        }
        else
        {
            existing.Name = request.Name;
            existing.ComplexId = request.ComplexId;
            existing.BuildingId = request.BuildingId;
            existing.Entrance = request.Entrance;
        }

        return Task.FromResult(new CreateScreenResponse
        {
            Status = "success",
            ScreenId = request.Id
        });
    }

    public Task<AssignScreensResponse> AssignTemplateAsync(AssignScreensRequest request)
    {
        var targetIds = request.ScreenIds.Contains("all", StringComparer.OrdinalIgnoreCase)
            ? _screens.Select(screen => screen.Id).ToList()
            : request.ScreenIds;

        foreach (var screen in _screens.Where(screen => targetIds.Contains(screen.Id)))
        {
            screen.CurrentTemplateId = request.TemplateId;
        }

        return Task.FromResult(new AssignScreensResponse
        {
            Status = "success",
            AssignedScreens = targetIds
        });
    }
}
