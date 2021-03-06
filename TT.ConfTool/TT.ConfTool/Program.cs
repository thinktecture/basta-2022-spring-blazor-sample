using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TT.ConfTool.Client;
using TT.ConfTool.Client.Conferences;
using TT.ConfTool.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

// TODO: Replace with config
builder.Services.AddHttpClient("WebAPI",
        client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseUrl")))
    .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
    .CreateClient("WebAPI"));


builder.Services.AddScoped<ConferencesClientService>();
builder.Services.AddScoped<CountriesService>();
builder.Services.AddSingleton<IDialogService, DialogService>();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Oidc", options.ProviderOptions);
});

builder.Services.AddMudServices();
await builder.Build().RunAsync();
