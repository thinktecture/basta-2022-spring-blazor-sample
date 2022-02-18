using Microsoft.AspNetCore.Components;

namespace TT.ConfTool.Client.Pages
{
    public partial class Index
    {
        [Inject] public NavigationManager _navigation { get; set; } = default!;
        private void StartLogin()
        {
            _navigation.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(_navigation.Uri)}");
        }
    }
}