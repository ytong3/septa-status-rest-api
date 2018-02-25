using System;
using System.Collections.Generic;
using SEPTAInquirer.POCO;

namespace SEPTAInquirer
{
    public interface IAlexaSpeakStrategy
    {
        string SayWhenLateForTheNextTrain(IEnumerable<TrainInfo> orderedTrainsToArrive, DateTime now);
        string SayWhenNotLateForTheNextTrain(IEnumerable<TrainInfo> orderedTrainsToArrive, DateTime now);
    }
}