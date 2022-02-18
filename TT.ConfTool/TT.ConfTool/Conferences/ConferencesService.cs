using System.Net.Http.Json;
using TT.ConfTool.Shared.DTO;

namespace TT.ConfTool.Client.Conferences
{
    public class ConferencesClientService
    {
        private HttpClient _httpClient;
        private string _conferencesUrl = "api/conferences/";

        public ConferencesClientService(IConfiguration config, HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ConferenceOverview>> GetConferencesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<ConferenceOverview>>(_conferencesUrl);

            return result;
        }

        public async Task<ConfTool.Shared.DTO.ConferenceDetails> GetConferenceDetailsAsync(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<ConfTool.Shared.DTO.ConferenceDetails>(_conferencesUrl + id);

            return result;
        }

        public async Task<ConfTool.Shared.DTO.ConferenceDetails> AddConferenceAsync(ConfTool.Shared.DTO.ConferenceDetails conference)
        {
            var result = await (await _httpClient.PostAsJsonAsync(
                _conferencesUrl, conference)).Content.ReadFromJsonAsync<ConfTool.Shared.DTO.ConferenceDetails>();

            return result;
        }

    }

}
