using System.Net.Http.Json;
using TT.ConfTool.Shared.DTO;

namespace TT.ConfTool.Client.Services
{
    public class ConferencesClientService
    {
        private HttpClient _httpClient;
        private string _conferencesUrl = "api/conferences/";

        public ConferencesClientService( IConfiguration config, HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ConferenceOverview>> GetConferencesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ConferenceOverview>>(_conferencesUrl);

            return result;
        }

        public async Task<ConferenceDetails> GetConferenceDetailsAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<ConferenceDetails>(_conferencesUrl + id);

            return result;
        }

        public async Task<ConferenceDetails> AddConferenceAsync(ConferenceDetails conference)
        {
            var result = await (await _httpClient.PostAsJsonAsync(
                _conferencesUrl, conference)).Content.ReadFromJsonAsync<ConferenceDetails>();

            return result;
        }

    }

}
