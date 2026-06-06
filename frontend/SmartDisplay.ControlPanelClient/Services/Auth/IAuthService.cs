using SmartDisplay.ControlPanelClient.Models.Auth;
using SmartDisplay.ControlPanelClient.Models.Common;

namespace SmartDisplay.ControlPanelClient.Services.Auth;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task<ApiStatusResponse> LogoutAsync();
}
