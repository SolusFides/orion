using System.Text.Json;
using Microsoft.JSInterop;
using SmartDisplay.ControlPanelClient.Models.Auth;

namespace SmartDisplay.ControlPanelClient.Services.Auth;

public class LocalStorageTokenStorageService : ITokenStorageService
{
    private const string StorageKey = "smart-display-admin-session";
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageTokenStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SaveSessionAsync(AdminSessionDto session)
    {
        var json = JsonSerializer.Serialize(session);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }

    public async Task<AdminSessionDto?> LoadSessionAsync()
    {
        var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", StorageKey);

        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        try
        {
            var session = JsonSerializer.Deserialize<AdminSessionDto>(json);
            return session is { IsAuthenticated: true } ? session : null;
        }
        catch
        {
            return null;
        }
    }

    public async Task ClearSessionAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", StorageKey);
    }
}
