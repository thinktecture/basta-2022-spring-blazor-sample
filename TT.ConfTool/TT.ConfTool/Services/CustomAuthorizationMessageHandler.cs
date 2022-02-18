using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace TT.ConfTool.Client.Services
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider
            , IConfiguration configuration
            , NavigationManager navigationManager)
            : base(provider, navigationManager)
        {
            var baseUrl = configuration.GetValue<string>("BaseUrl");
            ConfigureHandler(
                authorizedUrls: new[] { baseUrl },
                scopes: new[] { "api" });
        }
    }
}
