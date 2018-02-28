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
            var result = SayNextTrainInfo(nextTrain: orderedTrainsToArrive.First(), now: now);

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

            var trainStatus = $"{nextTrain.TrainStatus.ToString().ToLower()}";
            if (nextTrain.TrainStatus == TrainStatusEnum.Delayed)
                trainStatus =  trainStatus + $" by {nextTrain.LateInMinutes} minutes";
            else
                trainStatus = nextTrain.TrainStatus.ToString().ToLower();
            return
                $"{nextTrain.TrainName} Train is {trainStatus} and is leaving in {trainingLeavingInMinutes} minutes.";
        }
    }
}