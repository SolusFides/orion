using SmartDisplay.Shared.Models.Common;
using SmartDisplay.Shared.Models.Emergency;
using SmartDisplay.ControlPanelClient.Services.Common;

namespace SmartDisplay.ControlPanelClient.Services.Emergency;

public class LocalEmergencyService : IEmergencyService
{
    private readonly List<EmergencyActiveDto> _activeEmergencies = FallbackData.CreateActiveEmergencies();
    private readonly List<EmergencyLogDto> _log = FallbackData.CreateEmergencyLog();

    public Task<EmergencyActivateResponse> ActivateAsync(EmergencyActivateRequest request)
    {
        var emergencyId = $"uuid-emergency-{Guid.NewGuid():N}";

        // По ТЗ на одном экране одновременно не должно быть больше одного ЧС-сообщения.
        // Для локального режима упрощаем: новое сообщение заменяет все активные сообщения на выбранных целях.
        if (request.ScreenIds.Contains("all", StringComparer.OrdinalIgnoreCase))
        {
            _activeEmergencies.Clear();
        }
        else
        {
            _activeEmergencies.RemoveAll(item => item.TargetScreens.Any(screenId => request.ScreenIds.Contains(screenId)));
        }

        _activeEmergencies.Add(new EmergencyActiveDto
        {
            Id = emergencyId,
            Text = request.Text,
            TargetScreens = request.ScreenIds.ToList(),
            Priority = request.Priority,
            TimeoutMinutes = request.TimeoutMinutes,
            ActivatedBy = "admin",
            CreatedAt = DateTime.Now
        });

        _log.Insert(0, new EmergencyLogDto
        {
            Id = $"uuid-log-{Guid.NewGuid():N}",
            Action = "activate",
            UserId = "admin",
            TargetScreens = request.ScreenIds.ToList(),
            Text = request.Text,
            CreatedAt = DateTime.Now
        });

        return Task.FromResult(new EmergencyActivateResponse
        {
            Status = "success",
            EmergencyId = emergencyId
        });
    }

    public Task<ApiStatusResponse> ResetAsync(EmergencyResetRequest request)
    {
        if (request.ScreenIds.Contains("all", StringComparer.OrdinalIgnoreCase))
        {
            _activeEmergencies.Clear();
        }
        else
        {
            _activeEmergencies.RemoveAll(item => item.TargetScreens.Any(screenId => request.ScreenIds.Contains(screenId)));
        }

        _log.Insert(0, new EmergencyLogDto
        {
            Id = $"uuid-log-{Guid.NewGuid():N}",
            Action = "reset",
            UserId = "admin",
            TargetScreens = request.ScreenIds.ToList(),
            Text = null,
            CreatedAt = DateTime.Now
        });

        return Task.FromResult(new ApiStatusResponse
        {
            Status = "success"
        });
    }

    public Task<List<EmergencyActiveDto>> GetActiveAsync()
    {
        return Task.FromResult(_activeEmergencies.ToList());
    }

    public Task<List<EmergencyLogDto>> GetLogAsync()
    {
        return Task.FromResult(_log.ToList());
    }
}
