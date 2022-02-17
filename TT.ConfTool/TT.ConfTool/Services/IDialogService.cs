
namespace TT.ConfTool.Client.Services
{
    public interface IDialogService
    {
        Task AlertAsync(string message);
        Task<bool> ConfirmAsync(string message);
        ValueTask DisposeAsync();
    }
}