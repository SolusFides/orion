using System.Text.Json.Serialization;

namespace SmartDisplay.ControlPanelClient.Models.Auth;

public class LoginResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = "bearer";

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    public bool HasAccessToken => !string.IsNullOrWhiteSpace(AccessToken);

    public string AuthorizationHeaderValue => $"{TokenType} {AccessToken}";
}
