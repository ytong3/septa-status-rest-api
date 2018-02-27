using System.Collections.Generic;
using System.Threading.Tasks;
using SEPTAInquirer.POCO;

namespace SEPTAInquirer
{
    public interface ISeptapiClient
    {
        Task<IList<NextToArriveTrainDto>> GetNextToArriveFromHomeToDestinationAsync();
    }
}