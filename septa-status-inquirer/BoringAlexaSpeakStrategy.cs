using System;
using System.Collections.Generic;
using System.Linq;
using SEPTAInquirer.POCO;

namespace SEPTAInquirer
{
    public class BoringAlexaSpeakStrategy : IAlexaSpeakStrategy
    {
        public string SayWhenLateForTheNextTrain(IEnumerable<TrainInfo> orderedTrainsToArrive)
        {
            var result = SayNextTrainInfo(orderedTrainsToArrive.First());

            var secondNextTrain = orderedTrainsToArrive.ElementAt(1);
            result += "You are not goint to make it. Consider taking the next one. " +
                      SayNextTrainInfo(secondNextTrain);

            return result;
        }

        public string SayWhenNotLateForTheNextTrain(IEnumerable<TrainInfo> orderedTrainsToArrive)
        {
            var result = SayNextTrainInfo(orderedTrainsToArrive.First());

            result += "You can make it if you leave now.";

            return result;
        }

        private string SayNextTrainInfo(TrainInfo nextTrain)
        {
            return
                $"{nextTrain.TrainName} Train is {nextTrain.TrainStatu.ToString()} and is leaving in {nextTrain.NowDeparureTime.Subtract(DateTime.Now).TotalMinutes} minutes.";
        }
    }
}