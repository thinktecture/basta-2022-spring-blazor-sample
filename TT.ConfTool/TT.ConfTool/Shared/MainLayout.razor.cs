using MudBlazor;

namespace TT.ConfTool.Client.Shared
{
    public partial class MainLayout
    {
        private static readonly MudTheme DefaultTheme = new()
        {
            Palette = new Palette
            {
                Black = "#272c34",
                AppbarBackground = "#ffffff",
                AppbarText = "#ff584f",
                DrawerBackground = "#ff584f",
                DrawerText = "ffffff",
                DrawerIcon = "ffffff",
                Primary = "#ff584f",
                Secondary = "#3d6fb4"
            }
        };
    }
}