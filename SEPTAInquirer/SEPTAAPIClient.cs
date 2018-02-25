using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SEPTAInquirer;
using SEPTAInquirer.POCO;

namespace SEPTAInquierierForAlexa
{
    public class SEPTAAPIClient : ISeptapiClient
    {
        //TODO: make this runtime configuration or other best practice?
        private string _SeptaAPIBaseUrl = @"http://www3.septa.org/hackathon/";
        private string _destStation;
        private int _numberOfResults;
        private string _homeStation;
        private HttpClient _client;

        public SEPTAAPIClient(IConfiguration config)
        {
            _homeStation = config["SeptaAPIConfig:HomeStation"];
            _destStation = config["SeptaAPIConfig:DestStation"];
            try
            {
                // TODO: use strongly typed configurations
                _numberOfResults = int.Parse(config["SeptaAPIConfig:NumberOfResults"]);
            }
            catch(ArgumentException)
            {
                // log the error
            }

            _client = new HttpClient()
            {
                BaseAddress = new Uri(_SeptaAPIBaseUrl)
            };
        }

        public async Task<SEPTANextToArriveAPIResult> GetNextToArriveFromHomeToDestinationAsync()
        {
            var result = await _client.GetAsync(string.Format(@"NextToArrive/{0}/{1}/{2}", _homeStation, _destStation, _numberOfResults));

            //TODO: error handling
            var resultObject = JsonConvert.DeserializeObject<SEPTANextToArriveAPIResult>(result.Content.ToString());

            return resultObject;
        }
    }
}
