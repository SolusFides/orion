using System.Net.Http.Json;
using SmartDisplay.ControlPanelClient.Models.Auth;
using SmartDisplay.ControlPanelClient.Services.Common;
using SmartDisplay.Shared.Models.Common;

namespace SmartDisplay.ControlPanelClient.Services.Auth;

public class ApiAuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly AuthState _authState;
    private readonly ITokenStorageService _tokenStorage;

    public ApiAuthService(HttpClient httpClient, AuthState authState, ITokenStorageService tokenStorage)
    {
        _httpClient = httpClient;
        _authState = authState;
        _tokenStorage = tokenStorage;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        using var formContent = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["username"] = request.Username,
            ["password"] = request.Password
        });

        using var response = await _httpClient.PostAsync("api/auth/login", formContent);

        if (!response.IsSuccessStatusCode)
        {
            _authState.Clear();
            await _tokenStorage.ClearSessionAsync();
            return new LoginResponse();
        }

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(ApiJson.Options) ?? new LoginResponse();

        if (!loginResponse.HasAccessToken)
        {
            _authState.Clear();
            await _tokenStorage.ClearSessionAsync();
            return loginResponse;
        }

        _authState.StartSession(loginResponse, request.Username, "admin");

        if (_authState.CurrentSession is not null)
        {
            await _tokenStorage.SaveSessionAsync(_authState.CurrentSession);
        }

        return loginResponse;
    }

    public async Task<ApiStatusResponse> LogoutAsync()
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Post, "api/auth/logout");
            if (_authState.IsAuthenticated && !string.IsNullOrWhiteSpace(_authState.AccessToken))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authState.AccessToken);
            }

            using var response = await _httpClient.SendAsync(request);
            _authState.Clear();
            await _tokenStorage.ClearSessionAsync();

            return response.IsSuccessStatusCode
                ? new ApiStatusResponse { Status = "success" }
                : new ApiStatusResponse { Status = ApiError.Status };
        }
        catch
        {
            _authState.Clear();
            await _tokenStorage.ClearSessionAsync();
            return new ApiStatusResponse { Status = "success" };
        }
    }
}
