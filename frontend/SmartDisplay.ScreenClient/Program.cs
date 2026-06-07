using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SmartDisplay.ScreenClient;
using SmartDisplay.ScreenClient.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri(ScreenClientApiOptions.BaseUrl)
});

builder.Services.AddScoped<IClientScreenService, ApiClientScreenService>();

await builder.Build().RunAsync();
