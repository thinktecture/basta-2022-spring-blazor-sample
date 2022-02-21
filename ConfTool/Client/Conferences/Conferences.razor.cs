using ConfTool.Shared.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

namespace ConfTool.Client.Conferences
{
    public partial class Conferences
    {
        [Inject] private ConferencesService _conferencesService { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        private List<ConferenceOverview>? _conferences;
        private HubConnection _hubConnection;

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
            .WithUrl(_navigationManager.ToAbsoluteUri("/conferencesHub"))
            .Build();

            _hubConnection.On("NewConferenceAdded", async (Guid id) =>
            {
                Console.WriteLine("###SignalR - NEW conference added!");
                _conferences = await _conferencesService.GetConferencesAsync();

                StateHasChanged();
            });


            await _hubConnection.StartAsync();

            _conferences = await _conferencesService.GetConferencesAsync();
            await base.OnInitializedAsync();
        }

        private void AddConference()
        {
            _navigationManager.NavigateTo($"conferences/new");
        }

        private async Task LoadDetails(Guid id)
        {
            _navigationManager.NavigateTo($"conferences/show/{id}");
        }
    }
}