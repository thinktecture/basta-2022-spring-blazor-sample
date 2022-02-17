using Microsoft.AspNetCore.Components;

namespace TT.ConfTool.Client.Authentication
{
    public partial class RedirectToLogin
    {
        [Inject] public NavigationManager _navigation { get; set; } = default!;
        protected override void OnInitialized()
        {
            _navigation.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(_navigation.Uri)}");
        }
    }
}