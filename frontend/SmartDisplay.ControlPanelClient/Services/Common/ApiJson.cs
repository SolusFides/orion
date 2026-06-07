using System.Text.Json;

namespace SmartDisplay.ControlPanelClient.Services.Common;

public static class ApiJson
{
    public static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };
}
