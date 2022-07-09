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
            var loginCredentials = new UserLogin("admin", "admin_PW");
            var credentialJson = JsonConvert.SerializeObject(loginCredentials);
            var httpContent = new StringContent(credentialJson, Encoding.UTF8, "application/json");

            var loginResponse = await _client.PostAsync(EnpointsConstansts.LoginEndpoint, httpContent);
            if (loginResponse.IsSuccessStatusCode && loginResponse.Content != null)
            {
                var token = await loginResponse.Content.ReadAsStringAsync();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Trim('"'));
                return "Logged in";
            }

            return GetError(loginResponse);
        }

        internal async Task<string> GetLongestRuler()
        {
            var getResponse = await _client.GetAsync(EnpointsConstansts.LongestRulingMonarchEndpoint);
            if (getResponse.IsSuccessStatusCode)
            {
                var longestRuler = await getResponse.Content.ReadAsStringAsync();
                var longestRulerObj = JsonConvert.DeserializeObject<MonarchViewModel>(longestRuler);
                if (longestRulerObj != null)
                    return $"The longest ruling monarch is {longestRulerObj.FullName} who ruled for {longestRulerObj.NrOfYearsRuled}";
            }

            return GetError(getResponse);
        }

        internal async Task<string> GetNumberOfMonarchs()
        {
            var getResponse = await _client.GetAsync(EnpointsConstansts.NrOfMonarchsEndpoint);
            if (getResponse.IsSuccessStatusCode)
            {
                var nrOfRulers = await getResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(nrOfRulers))
                    return $"The number of monarchs is {nrOfRulers}";
            }

            return GetError(getResponse);
        }

        internal async Task<string> GetLongestRulingHouse()
        {
            var getResponse = await _client.GetAsync(EnpointsConstansts.LongestRulingHouseEndpoint);
            if (getResponse.IsSuccessStatusCode)
            {
                var longestHouse = await getResponse.Content.ReadAsStringAsync();
                var houseViewModel = JsonConvert.DeserializeObject<HouseViewModel>(longestHouse);
                if (houseViewModel != null)
                    return $"The longest ruling house is {houseViewModel.House} who ruled for {houseViewModel.NrOfYearsRuled}";
            }

            return GetError(getResponse);
        }

        internal async Task<string> GetMostCommonFirstName()
        {
            var getResponse = await _client.GetAsync(EnpointsConstansts.LongestRulingHouseEndpoint);
            if (getResponse.IsSuccessStatusCode)
            {
                var name = await getResponse.Content.ReadAsStringAsync();
                return $"The most common name for a monarch is {name.Trim('"')}";
            }

            return GetError(getResponse);
        }

        private string GetError(HttpResponseMessage httpResponseMessage) =>
             $"Request failed with code: {httpResponseMessage.StatusCode} and message: {httpResponseMessage.ReasonPhrase}";

    }
}
