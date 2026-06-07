using SmartDisplay.Shared.Models.ClientDisplay;

namespace SmartDisplay.ScreenClient.Services;

public interface IClientScreenService
{
    Task<ClientScreenResponse> GetScreenAsync(string screenId);
    Task<ClientScreenResponse> GetPreviewAsync(string templateId);
}
