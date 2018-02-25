using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SEPTAInquirer.POCO;

namespace SEPTAInquirer
{
    public interface ISeptaSpeechGenerator
    {
        string GenerateSpeechForAlexa(IEnumerable<TrainInfo> trainsToArrive, DateTime utcNow);
    }

    // TODO: abstract base class or strategy pattern? composition over inheritance.
    public class SpetaSpeechGenerator : ISeptaSpeechGenerator
    {
        private IAlexaSpeakStrategy _speakStrategy;

        public SpetaSpeechGenerator(IAlexaSpeakStrategy speakStrategy)
        {
            _speakStrategy = speakStrategy;
        }

        public string GenerateSpeechForAlexa(IEnumerable<TrainInfo> trainsToArrive, DateTime utcNow)
        {
            // order the IEnumerable
            var orderedListOfArrivingTrains = trainsToArrive.OrderBy(train => train.NowDeparureTime);

            // convert utcNow to EST
            var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("EST");
            var localNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, estTimeZone);
            
            var theVeryNextTrain = orderedListOfArrivingTrains.First();

            var minutesToGo = theVeryNextTrain.NowDeparureTime.Subtract(localNow).TotalMinutes;

            if (minutesToGo < 7)
            {
                return _speakStrategy.SayWhenLateForTheNextTrain(orderedListOfArrivingTrains, localNow);
            }
            else
            {
                return _speakStrategy.SayWhenNotLateForTheNextTrain(orderedListOfArrivingTrains, localNow);
            }
        }
    }
}