using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using SmartDisplay.ControlPanelClient.Services.Auth;

namespace SmartDisplay.ControlPanelClient.Services.Common;

public class AuthorizedApiClient
{
    private readonly HttpClient _httpClient;
    private readonly AuthState _authState;
    private readonly ITokenStorageService _tokenStorage;

    public AuthorizedApiClient(HttpClient httpClient, AuthState authState, ITokenStorageService tokenStorage)
    {
        _httpClient = httpClient;
        _authState = authState;
        _tokenStorage = tokenStorage;
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        await AddAuthorizationAsync(request);
        return await SendAsync<T>(request);
    }

    public async Task<T?> PostAsync<T>(string url, object body)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = JsonContent.Create(body, options: ApiJson.Options)
        };

        await AddAuthorizationAsync(request);
        return await SendAsync<T>(request);
    }

    public async Task<T?> PutAsync<T>(string url, object body)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = JsonContent.Create(body, options: ApiJson.Options)
        };

        await AddAuthorizationAsync(request);
        return await SendAsync<T>(request);
    }

    public async Task<T?> PatchAsync<T>(string url)
    {
        using var request = new HttpRequestMessage(HttpMethod.Patch, url);
        await AddAuthorizationAsync(request);
        return await SendAsync<T>(request);
    }

    private async Task AddAuthorizationAsync(HttpRequestMessage request)
    {
        if (!_authState.IsAuthenticated)
        {
            var savedSession = await _tokenStorage.LoadSessionAsync();
            if (savedSession is not null)
            {
                _authState.RestoreSession(savedSession);
            }
        }

        if (_authState.IsAuthenticated && !string.IsNullOrWhiteSpace(_authState.AccessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authState.AccessToken);
        }
    }

    private async Task<T?> SendAsync<T>(HttpRequestMessage request)
    {
        using var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"API request failed: {(int)response.StatusCode} {response.ReasonPhrase}. {content}");
        }

        if (string.IsNullOrWhiteSpace(content))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(content, ApiJson.Options);
    }
}
