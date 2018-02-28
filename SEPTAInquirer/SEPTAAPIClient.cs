using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SEPTAInquirer;
using SEPTAInquirer.Configurations;
using SEPTAInquirer.POCO;

namespace SEPTAInquierierForAlexa
{
    public class SEPTAAPIClient : ISeptapiClient
    {
        static string _SeptaAPIBaseUrl = @"http://www3.septa.org/hackathon/";
        private string _destStation;
        private int _numberOfResults;
        private string _homeStation;
        private HttpClient _client;

        public SEPTAAPIClient(IOptions<SeptaApiConfig> config)
        {
                _homeStation = config.Value.HomeStation;
                _destStation = config.Value.DestStation;
                _destStation = config.Value.DestStation;
                _numberOfResults = config.Value.NumberOfResultsToGet;
                _SeptaAPIBaseUrl = config.Value.ApiBaseUri;

            _client = new HttpClient()
            {
                BaseAddress = new Uri(_SeptaAPIBaseUrl)
            };
        }

        public async Task<IList<NextToArriveTrainDto>> GetNextToArriveFromHomeToDestinationAsync()
        {
            var result = await _client.GetAsync(string.Format(@"NextToArrive/{0}/{1}/{2}", _homeStation, _destStation, _numberOfResults));

            result.EnsureSuccessStatusCode();

            var resultObject = JsonConvert.DeserializeObject<IList<NextToArriveTrainDto>>(await result.Content.ReadAsStringAsync());

            return resultObject;
        }
    }
}
