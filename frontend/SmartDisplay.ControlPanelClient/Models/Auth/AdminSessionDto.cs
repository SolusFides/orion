namespace SmartDisplay.ControlPanelClient.Models.Auth;

public class AdminSessionDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string TokenType { get; set; } = "bearer";
    public int ExpiresIn { get; set; }
    public DateTime LoginAt { get; set; } = DateTime.Now;
    public string UserName { get; set; } = "admin";
    public string Role { get; set; } = "admin";

    public DateTime ExpiresAt => LoginAt.AddSeconds(ExpiresIn);
    public bool IsExpired => DateTime.Now >= ExpiresAt;
    public bool IsAuthenticated => !string.IsNullOrWhiteSpace(AccessToken) && !IsExpired;

    public string AuthorizationHeaderValue => $"{TokenType} {AccessToken}";
}
