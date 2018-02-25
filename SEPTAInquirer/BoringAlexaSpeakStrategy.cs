using System;
using System.Collections.Generic;
using System.Linq;
using SEPTAInquirer.POCO;

namespace SEPTAInquirer
{
    public class BoringAlexaSpeakStrategy : IAlexaSpeakStrategy
    {
        public string SayWhenLateForTheNextTrain(IEnumerable<TrainInfo> orderedTrainsToArrive, DateTime now)
        {
            var result = SayNextTrainInfo(orderedTrainsToArrive.First(), now);

            var secondNextTrain = orderedTrainsToArrive.ElementAt(1);
            result += " You are not going to make it. Consider taking the next one. " +
                      SayNextTrainInfo(secondNextTrain, now);

            return result;
        }

        public string SayWhenNotLateForTheNextTrain(IEnumerable<TrainInfo> orderedTrainsToArrive, DateTime now)
        {
            var result = SayNextTrainInfo(orderedTrainsToArrive.First(), now);

            result += " You can make it if you leave now.";

            return result;
        }

        private string SayNextTrainInfo(TrainInfo nextTrain, DateTime now)
        {
            var trainingLeavingInMinutes = Math.Floor(nextTrain.NowDeparureTime.Subtract(now).TotalMinutes);
            return
                $"{nextTrain.TrainName} Train is {nextTrain.TrainStatus.ToString().ToLower()} and is leaving in {trainingLeavingInMinutes} minutes.";
        }
    }
}