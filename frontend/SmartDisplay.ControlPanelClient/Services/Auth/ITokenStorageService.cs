using SmartDisplay.ControlPanelClient.Models.Auth;

namespace SmartDisplay.ControlPanelClient.Services.Auth;

public interface ITokenStorageService
{
    Task SaveSessionAsync(AdminSessionDto session);
    Task<AdminSessionDto?> LoadSessionAsync();
    Task ClearSessionAsync();
}
