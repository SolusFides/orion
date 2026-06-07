using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmartDisplay.ControlPanelClient;
using SmartDisplay.ControlPanelClient.Services.Auth;
using SmartDisplay.ControlPanelClient.Services.Common;
using SmartDisplay.ControlPanelClient.Services.Dashboard;
using SmartDisplay.ControlPanelClient.Services.Emergency;
using SmartDisplay.ControlPanelClient.Services.Screens;
using SmartDisplay.ControlPanelClient.Services.Templates;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(ApiOptions.BaseUrl)
});

builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<ITokenStorageService, LocalStorageTokenStorageService>();
builder.Services.AddScoped<AuthorizedApiClient>();

builder.Services.AddScoped<IAuthService, ApiAuthService>();
builder.Services.AddScoped<IScreensService, ApiScreensService>();
builder.Services.AddScoped<ITemplatesService, ApiTemplatesService>();
builder.Services.AddScoped<IEmergencyService, ApiEmergencyService>();
builder.Services.AddScoped<IDashboardService, ApiDashboardService>();

await builder.Build().RunAsync();
