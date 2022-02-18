using Microsoft.AspNetCore.Components;
using TT.ConfTool.Client.Services;

namespace TT.ConfTool.Client.Conferences
{
    public partial class ConferenceDetails
    {
        [Parameter]
        public Guid Id { get; set; }

        [Parameter]
        public string Mode { get; set; }


        [Inject]
        private ConferencesClientService _conferencesClient { get; set; } = default!;

        [Inject]
        private CountriesService _countriesClient { get; set; } = default!;

        [Inject]
        private NavigationManager _navigationManager { get; set; } = default!;

        [Inject]
        private IDialogService _dialog { get; set; } = default!;

        private bool _isShow;
        private ConfTool.Shared.DTO.ConferenceDetails _conferenceDetails = new ConfTool.Shared.DTO.ConferenceDetails();
        private List<string> _countries;

        public ConferenceDetails()
        {
            _conferenceDetails = new ConfTool.Shared.DTO.ConferenceDetails();
            _conferenceDetails.DateFrom = DateTime.UtcNow;
            _conferenceDetails.DateTo = DateTime.UtcNow;
        }


        protected override async Task OnInitializedAsync()
        {
            _isShow = Mode == ConferenceDetailsModes.Show;

            if (Mode == ConferenceDetailsModes.Show || Mode == ConferenceDetailsModes.Edit)
            {
                _conferenceDetails = await _conferencesClient.GetConferenceDetailsAsync(Id);
            }
            if (Mode == ConferenceDetailsModes.New || Mode == ConferenceDetailsModes.Edit)
            {
                _countries = await _countriesClient.ListCountriesAsync();
            }
        }

        private async Task<IEnumerable<string>> SearchCountry(string value)
        {
            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _countries;

            return _countries.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }

        private async Task SaveConference()
        {
            if (!await _dialog.ConfirmAsync("Do you want to save this new entry?"))
            {
                Console.WriteLine("### User declined to save conference!");
                return;
            }

            if (Mode == ConferenceDetailsModes.New)
            {
                await _conferencesClient.AddConferenceAsync(_conferenceDetails);
                Console.WriteLine("NEW Conference added...");
            }
            _navigationManager.NavigateTo("/conferences");
        }

        private void Cancel()
        {
            _navigationManager.NavigateTo("/conferences");
        }

    }
}