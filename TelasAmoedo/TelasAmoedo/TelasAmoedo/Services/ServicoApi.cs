using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TelasAmoedo.Models;

namespace TelasAmoedo.Services
{
    public class ServicoApi
    {
        const string Url = "https://api.airtable.com/v0/appjgrPVA2ykGwCJk/Table%201";
        const string API_KEY = "keyPmk2OP18Q9fHWy";

        readonly HttpClient _httpClient;

        public ServicoApi()
        {
            _httpClient = new HttpClient();


            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", API_KEY);
        }

        public async Task LoginAsync(string email, string senha)
        {
            var response = await _httpClient.GetAsync(Url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var records = JsonConvert.DeserializeObject<AirtableResponse>(content)?.Records;
                var result = records?.Select(x => x.Fields);
            }

        }
    }
}
