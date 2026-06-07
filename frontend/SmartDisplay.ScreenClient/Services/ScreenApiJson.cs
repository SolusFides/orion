using System.Text.Json;

namespace SmartDisplay.ScreenClient.Services;

public static class ScreenApiJson
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };
}
