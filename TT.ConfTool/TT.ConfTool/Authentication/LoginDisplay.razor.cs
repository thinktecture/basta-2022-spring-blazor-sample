using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace TT.ConfTool.Client.Authentication
{
    public partial class LoginDisplay
    {
        [Inject] public NavigationManager _navigation { get; set; } = default!;
        [Inject] public SignOutSessionStateManager _signOutManager { get; set; } = default!;

        private async Task BeginSignOut(MouseEventArgs args)
        {
            await _signOutManager.SetSignOutState();
            _navigation.NavigateTo("authentication/logout");
        }
    }
}