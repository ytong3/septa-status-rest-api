using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SEPTAInquirer.POCO;

namespace SEPTAInquirer
{
    public interface ISeptaSpeechGenerator
    {
        string GenerateSpeechForAlexa(IEnumerable<TrainInfo> trainsToArrive, DateTime now);
    }

    // TODO: abstract base class or strategy pattern? composition over inheritance.
    public class SpetaSpeechGenerator : ISeptaSpeechGenerator
    {
        private IAlexaSpeakStrategy _speakStrategy;

        public SpetaSpeechGenerator(IAlexaSpeakStrategy speakStrategy)
        {
            _speakStrategy = speakStrategy;
        }

        public string GenerateSpeechForAlexa(IEnumerable<TrainInfo> trainsToArrive, DateTime now)
        {
            // order the IEnumerable
            var orderedListOfArrivingTrains = trainsToArrive.OrderBy(train => train.NowDeparureTime);

            // TODO: usage of the IEnumerable?
            var theVeryNextTrain = orderedListOfArrivingTrains.First();

            var minutesToGo = theVeryNextTrain.NowDeparureTime.Subtract(now).TotalMinutes;

            if (minutesToGo < 7)
            {
                return _speakStrategy.SayWhenLateForTheNextTrain(orderedListOfArrivingTrains, now);
            }
            else
            {
                return _speakStrategy.SayWhenNotLateForTheNextTrain(orderedListOfArrivingTrains, now);
            }
        }
    }
}