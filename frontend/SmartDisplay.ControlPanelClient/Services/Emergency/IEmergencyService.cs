using SmartDisplay.ControlPanelClient.Models.Common;
using SmartDisplay.ControlPanelClient.Models.Emergency;

namespace SmartDisplay.ControlPanelClient.Services.Emergency;

public interface IEmergencyService
{
    Task<EmergencyActivateResponse> ActivateAsync(EmergencyActivateRequest request);
    Task<ApiStatusResponse> ResetAsync(EmergencyResetRequest request);
    Task<List<EmergencyActiveDto>> GetActiveAsync();
    Task<List<EmergencyLogDto>> GetLogAsync();
}
