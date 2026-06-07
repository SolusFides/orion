using SmartDisplay.ControlPanelClient.Services.Common;
using SmartDisplay.Shared.Models.Common;
using SmartDisplay.Shared.Models.Emergency;

namespace SmartDisplay.ControlPanelClient.Services.Emergency;

public class ApiEmergencyService : IEmergencyService
{
    private readonly AuthorizedApiClient _apiClient;

    public ApiEmergencyService(AuthorizedApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    public async Task<EmergencyActivateResponse> ActivateAsync(EmergencyActivateRequest request)
    {
        try
        {
            var emergency = await _apiClient.PostAsync<EmergencyActiveDto>("api/emergency/activate", request);
            return emergency is null
                ? new EmergencyActivateResponse { Status = ApiError.Status }
                : new EmergencyActivateResponse { Status = "success", EmergencyId = emergency.Id };
        }
        catch
        {
            return new EmergencyActivateResponse { Status = ApiError.Status };
        }
    }

    public async Task<ApiStatusResponse> ResetAsync(EmergencyResetRequest request)
    {
        try
        {
            await _apiClient.PostAsync<string>("api/emergency/reset", request);
            return new ApiStatusResponse { Status = "success" };
        }
        catch
        {
            return new ApiStatusResponse { Status = ApiError.Status };
        }
    }

    public async Task<List<EmergencyActiveDto>> GetActiveAsync()
    {
        try
        {
            return await _apiClient.GetAsync<List<EmergencyActiveDto>>("api/emergency/active") ?? new List<EmergencyActiveDto>();
        }
        catch
        {
            return FallbackData.CreateActiveEmergencies();
        }
    }

    public async Task<List<EmergencyLogDto>> GetLogAsync()
    {
        try
        {
            return await _apiClient.GetAsync<List<EmergencyLogDto>>("api/emergency/logs") ?? new List<EmergencyLogDto>();
        }
        catch
        {
            return FallbackData.CreateEmergencyLog();
        }
    }
}
