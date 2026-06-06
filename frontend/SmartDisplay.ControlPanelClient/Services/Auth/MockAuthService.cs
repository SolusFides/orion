using SmartDisplay.ControlPanelClient.Models.Auth;
using SmartDisplay.ControlPanelClient.Models.Common;

namespace SmartDisplay.ControlPanelClient.Services.Auth;

public class MockAuthService : IAuthService
{
    private readonly AuthState _authState;

    public MockAuthService(AuthState authState)
    {
        _authState = authState;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        await Task.Delay(300);

        var isValid = request.Username == "admin" && request.Password == "secret";

        if (!isValid)
        {
            _authState.Clear();
            return new LoginResponse();
        }

        var response = new LoginResponse
        {
            AccessToken = "mock-admin-access-token",
            TokenType = "bearer",
            ExpiresIn = 86400
        };

        _authState.StartSession(response, request.Username, "admin");
        return response;
    }

    public Task<ApiStatusResponse> LogoutAsync()
    {
        _authState.Clear();

        return Task.FromResult(new ApiStatusResponse
        {
            Status = "success"
        });
    }
}
