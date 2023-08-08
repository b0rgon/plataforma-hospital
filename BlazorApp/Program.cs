using BlazorApp;
using BlazorApp.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7183/") });
builder.Services.AddScoped<ApiService>();

await builder.Build().RunAsync();