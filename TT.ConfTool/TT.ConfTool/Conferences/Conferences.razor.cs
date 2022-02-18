using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace TT.ConfTool.Client.Conferences
{
    public partial class Conferences
    {
        [Inject]
        private ConferencesClientService _conferencesClientService { get; set; } = default!;

        [Inject]
        private NavigationManager _navigationManager { get; set; } = default!;

        private List<ConfTool.Shared.DTO.ConferenceOverview>? _conferences;
        private HubConnection? _hubConnection;
        private Guid _newId = Guid.Empty;

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:7156/conferencesHub")
                    .Build();

            _hubConnection.On("NewConferenceAdded", async (string id) =>
            {
                Console.WriteLine($"###SignalR - NEW conference added. Id: {id}");
                _newId = new Guid(id);
                await ListConferences();
                await InvokeAsync(StateHasChanged);
            });

            await _hubConnection.StartAsync();

            await ListConferences();
        }

        private async Task ListConferences()
        {
            var result = await _conferencesClientService.GetConferencesAsync();
            if (result != null)
            {
                _conferences = result.ToList();
            }
        }

        private void LoadDetails(string mode, Guid? id = null)
        {
            _navigationManager.NavigateTo($"conferences/{mode}/{id ?? Guid.NewGuid()}");
        }      
    }
}