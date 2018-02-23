using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SEPTAInquirer.POCO;

namespace SEPTAInquirer
{
    public interface ISeptaSpeechGenerator
    {
        string GenerateSpeechForAlexa(IEnumerable<TrainInfo> trainsToArrive);
    }

    // TODO: abstract base class or strategy pattern? composition over inheritance.
    public class SpetaSpeechGenerator : ISeptaSpeechGenerator
    {
        private ISeptapiClient _apiClient;
        private IAlexaSpeakStrategy _speakStrategy;

        protected SpetaSpeechGenerator(IAlexaSpeakStrategy speakStrategy)
        {
            _speakStrategy = speakStrategy;
        }

        public string GenerateSpeechForAlexa(IEnumerable<TrainInfo> trainsToArrive)
        {
            // order the IEnumerable
            var orderedListOfArrivingTrains = trainsToArrive.OrderBy(train => train.NowDeparureTime);

            // TODO: usage of the IEnumerable?
            var theVeryNextTrain = orderedListOfArrivingTrains.First();

            var minutesToGo = theVeryNextTrain.NowDeparureTime.Subtract(DateTime.Now).TotalMinutes;

            if (minutesToGo < 7)
            {
                return _speakStrategy.SayWhenLateForTheNextTrain(orderedListOfArrivingTrains);
            }
            else
            {
                return _speakStrategy.SayWhenNotLateForTheNextTrain(orderedListOfArrivingTrains);
            }
        }
    }
}