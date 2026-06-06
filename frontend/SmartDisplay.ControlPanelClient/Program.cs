using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmartDisplay.ControlPanelClient;
using SmartDisplay.ControlPanelClient.Services.Auth;
using SmartDisplay.ControlPanelClient.Services.Screens;
using SmartDisplay.ControlPanelClient.Services.Templates;
using SmartDisplay.ControlPanelClient.Services.Emergency;
using SmartDisplay.ControlPanelClient.Services.Dashboard;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<IAuthService, LocalAuthService>();

builder.Services.AddScoped<IScreensService, LocalScreensService>();
builder.Services.AddScoped<ITemplatesService, LocalTemplatesService>();
builder.Services.AddScoped<IEmergencyService, LocalEmergencyService>();
builder.Services.AddScoped<IDashboardService, LocalDashboardService>();

await builder.Build().RunAsync();