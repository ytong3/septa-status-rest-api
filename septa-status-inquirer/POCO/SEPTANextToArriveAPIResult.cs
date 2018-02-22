using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace SEPTAInquirer.POCO
{
    public class SEPTANextToArriveAPIResult
    {
        public IEnumerable<NextToArriveTrainTimeInfo> TrainsToArriveAtHomeStation { get; set; }
    };

    public class NextToArriveTrainTimeInfo
    {
        public string orig_departure_time { get; set; }
        public string orig_delay { get; set; }
    }
}