using SmartDisplay.Shared.Models.Common;
using SmartDisplay.Shared.Models.Emergency;

namespace SmartDisplay.ControlPanelClient.Services.Emergency;

public interface IEmergencyService
{
    Task<EmergencyActivateResponse> ActivateAsync(EmergencyActivateRequest request);
    Task<ApiStatusResponse> ResetAsync(EmergencyResetRequest request);
    Task<List<EmergencyActiveDto>> GetActiveAsync();
    Task<List<EmergencyLogDto>> GetLogAsync();
}
