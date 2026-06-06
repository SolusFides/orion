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
builder.Services.AddScoped<IAuthService, MockAuthService>();

builder.Services.AddScoped<IScreensService, MockScreensService>();
builder.Services.AddScoped<ITemplatesService, MockTemplatesService>();
builder.Services.AddScoped<IEmergencyService, MockEmergencyService>();
builder.Services.AddScoped<IDashboardService, MockDashboardService>();

await builder.Build().RunAsync();