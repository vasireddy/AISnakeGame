using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AISnakeStudio;
using AISnakeStudio.GameEngine;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register the AI Generator as a service
builder.Services.AddScoped<IGameConfigurationGenerator, TransformersJsConfigGenerator>();

await builder.Build().RunAsync();
