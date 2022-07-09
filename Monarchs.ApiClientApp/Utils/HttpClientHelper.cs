using Monarchs.Common.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Monarchs.ApiClientApp.Utils
{
    internal class HttpClientHelper
    {
        private readonly HttpClient _client;

        public HttpClientHelper(HttpClient client)
        {
            _client = client;
        }

        internal async Task<string> Login()
        {
            var loginEndpoint = "https://localhost:7068/Login";
            var loginCredentials = new UserLogin("admin", "admin_PW");
            var credentialJson = JsonConvert.SerializeObject(loginCredentials);
            var httpContent = new StringContent(credentialJson, Encoding.UTF8, "application/json");

            var loginResponse = await _client.PostAsync(loginEndpoint, httpContent);
            if (loginResponse.IsSuccessStatusCode && loginResponse.Content != null)
            {
                var token = await loginResponse.Content.ReadAsStringAsync();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
                return "Logged in";
            }

            return "Login failed";
        }

        internal async Task<string> GetLongestRuler()
        {
            var longestRulingMonarchEndpoint = "https://localhost:7068/Monarchs/GetLongestRulingMonarch";
            var getLongestResponse = await _client.GetAsync(longestRulingMonarchEndpoint);
            if (getLongestResponse.IsSuccessStatusCode)
            {
                var longestRuler = await getLongestResponse.Content.ReadAsStringAsync();
                var longestRulerObj = JsonConvert.DeserializeObject<MonarchViewModel>(longestRuler);
                if (longestRulerObj != null)
                    return $"The longest ruling monarch is {longestRulerObj.FullName} who ruled for {longestRulerObj.NrOfYearsRuled}";
            }

            if (getLongestResponse.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                return "Unauthorized";

            return string.Empty;
        }

        internal async Task<string> GetNumberOfMonarchs()
        {
            var NrOfMonarchsEndpoint = "https://localhost:7068/Monarchs/GetNumberOfMonarchs";
            var getResponse = await _client.GetAsync(NrOfMonarchsEndpoint);
            if (getResponse.IsSuccessStatusCode)
            {
                var nrOfRulers = await getResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(nrOfRulers))
                    return $"The number of monarchs is {nrOfRulers}";
            }

            if (getResponse.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                return "Unauthorized";

            return string.Empty;
        }

        internal async Task<string> GetLongestRulingHouse()
        {
            var longestRulingHouseEndpoint = "https://localhost:7068/Monarchs/GetLongestRulingHouse";
            var getResponse = await _client.GetAsync(longestRulingHouseEndpoint);
            if (getResponse.IsSuccessStatusCode)
            {
                var longestHouse = await getResponse.Content.ReadAsStringAsync();
                var houseViewModel = JsonConvert.DeserializeObject<HouseViewModel>(longestHouse);
                if (houseViewModel != null)
                    return $"The longest ruling house is {houseViewModel.House} who ruled for {houseViewModel.NrOfYearsRuled}";
            }

            if (getResponse.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                return "Unauthorized";

            return string.Empty;
        }

        internal async Task<string> GetMostCommonFirstName()
        {
            var longestRulingHouseEndpoint = "https://localhost:7068/Monarchs/GetMostCommonFirstName";
            var getResponse = await _client.GetAsync(longestRulingHouseEndpoint);
            if (getResponse.IsSuccessStatusCode)
            {
                var name = await getResponse.Content.ReadAsStringAsync();
                return $"The most common name for a monarch is {name.Trim('"')}";
            }

            if (getResponse.StatusCode is System.Net.HttpStatusCode.Unauthorized)
                return "Unauthorized";

            return string.Empty;
        }

    }
}
