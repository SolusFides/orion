using SmartDisplay.ControlPanelClient.Models.Auth;

namespace SmartDisplay.ControlPanelClient.Services.Auth;

public class AuthState
{
    public AdminSessionDto? CurrentSession { get; private set; }

    public bool IsAuthenticated => CurrentSession?.IsAuthenticated == true;
    public string AccessToken => CurrentSession?.AccessToken ?? string.Empty;
    public string AuthorizationHeaderValue => CurrentSession?.AuthorizationHeaderValue ?? string.Empty;

    public void StartSession(LoginResponse response)
    {
        if (!response.HasAccessToken)
        {
            Clear();
            return;
        }

        CurrentSession = new AdminSessionDto
        {
            AccessToken = response.AccessToken,
            TokenType = response.TokenType,
            ExpiresIn = response.ExpiresIn,
            LoginAt = DateTime.Now
        };
    }

    public void Clear()
    {
        CurrentSession = null;
    }
}
