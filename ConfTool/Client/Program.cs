using ConfTool.Client;
using ConfTool.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using ConfTool.Client.Components.Webcam;
using ConfTool.Client.Conferences;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("ConfTool.ServerAPI", client =>
                client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => 
    sp.GetRequiredService<IHttpClientFactory>()
        .CreateClient("ConfTool.ServerAPI"));

builder.Services.AddMudServices();
builder.Services.AddWebcam();
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Oidc", options.ProviderOptions);
});

builder.Services.AddScoped<ConferencesService>();
builder.Services.AddScoped<CountriesService>();
builder.Services.AddSingleton<DialogService>();

await builder.Build().RunAsync();
