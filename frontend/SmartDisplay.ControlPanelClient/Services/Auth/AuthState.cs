using SmartDisplay.ControlPanelClient.Models.Auth;

namespace SmartDisplay.ControlPanelClient.Services.Auth;

public class AuthState
{
    public AdminSessionDto? CurrentSession { get; private set; }

    public bool IsAuthenticated => CurrentSession?.IsAuthenticated == true;
    public string AccessToken => CurrentSession?.AccessToken ?? string.Empty;
    public string AuthorizationHeaderValue => CurrentSession?.AuthorizationHeaderValue ?? string.Empty;

    public string UserName { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;

    public void StartSession(LoginResponse response)
    {
        StartSession(response, "admin", "admin");
    }

    public void StartSession(LoginResponse response, string userName, string role)
    {
        if (!response.HasAccessToken)
        {
            Clear();
            return;
        }

        var session = new AdminSessionDto
        {
            AccessToken = response.AccessToken,
            TokenType = string.IsNullOrWhiteSpace(response.TokenType) ? "bearer" : response.TokenType,
            ExpiresIn = response.ExpiresIn <= 0 ? 86400 : response.ExpiresIn,
            LoginAt = DateTime.Now,
            UserName = string.IsNullOrWhiteSpace(userName) ? "admin" : userName,
            Role = string.IsNullOrWhiteSpace(role) ? "admin" : role
        };

        RestoreSession(session);
    }

    public void RestoreSession(AdminSessionDto session)
    {
        if (!session.IsAuthenticated)
        {
            Clear();
            return;
        }

        CurrentSession = session;
        UserName = string.IsNullOrWhiteSpace(session.UserName) ? "admin" : session.UserName;
        Role = string.IsNullOrWhiteSpace(session.Role) ? "admin" : session.Role;
    }

    public void Clear()
    {
        CurrentSession = null;
        UserName = string.Empty;
        Role = string.Empty;
    }
}
