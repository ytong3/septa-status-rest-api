using System.Collections.Generic;
using SEPTAInquirer.POCO;

namespace SEPTAInquirer
{
    public interface IAlexaSpeakStrategy
    {
        string SayWhenLateForTheNextTrain(IEnumerable<TrainInfo> orderedTrainsToArrive);
        string SayWhenNotLateForTheNextTrain(IEnumerable<TrainInfo> orderedTrainsToArrive);
    }
}