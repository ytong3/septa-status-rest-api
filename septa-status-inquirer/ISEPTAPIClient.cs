using System.Threading.Tasks;
using SEPTAInquirer.POCO;

namespace SEPTAInquirer
{
    public interface ISeptapiClient
    {
        Task<SEPTANextToArriveAPIResult> GetNextToArriveFromHomeToDestinationAsync();
    }
}