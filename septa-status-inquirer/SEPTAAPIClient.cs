using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SEPTAInquierierForAlexa
{
    public interface ISEPTAPIClient
    {
        TrainStatusForAlexaDto GetTrainStatus(IEnumerable<string> trainNumber);
        TrainStatusForAlexaDto GetNexTrains(string fromStation);
    }
    public class SEPTAAPIClient : ISEPTAPIClient
    {
        //TODO: make this runtime configuration or other best practice?
        private string _nextToArriveAPIUrlFormat = @"http://www3.septa.org/hackathon/NextToArrive/{0}/{1}/{2}";
        private string _destStation;
        private string _homeStation;

        public SEPTAAPIClient(IConfiguration config)
        {
            _homeStation = config["SeptaAPIConfig:HomeStation"];
            _destStation = config["SeptaAPIConfig:DestStation"];
        }
        public TrainStatusForAlexaDto GetNexTrains(string fromStation)
        {
            throw new NotImplementedException();
        }

        public TrainStatusForAlexaDto GetTrainStatus(IEnumerable<string> trainNumber)
        {
            throw new NotImplementedException();
        }
    }
}
