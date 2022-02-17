﻿using System.Net.Http.Json;

namespace TT.ConfTool.Client.Services
{
    public class CountriesClientService
    {
        private HttpClient _httpClient;
        private string _baseApiUrl;

        public CountriesClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> ListCountriesAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<List<string>>("api/countries/");

            return result;
        }
    }

}
