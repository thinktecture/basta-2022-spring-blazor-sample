using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using TT.ConfTool.Client.Models;
using TT.ConfTool.Client.Services;
using DTO = TT.ConfTool.Shared.DTO;

namespace TT.ConfTool.Client.Pages
{
    public partial class Index
    {
        [Inject] 
        private ConferencesClientService _conferencesClientService { get; set; } = default!;

        [Inject]
        private NavigationManager _navigationManager { get; set; } = default!;

        private List<DTO.ConferenceOverview>? _conferences;
        private HubConnection? _hubConnection;

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:7156/conferencesHub")
                    .Build();

            _hubConnection.On("NewConferenceAdded", async () =>
            {
                Console.WriteLine("###SignalR - NEW conference added!");
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

        private void LoadDetails(string mode, Guid? id =  null)
        {
            _navigationManager.NavigateTo($"conferences/{mode}/{id ?? Guid.NewGuid()}");
        }
    }
}