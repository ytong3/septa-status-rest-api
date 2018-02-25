using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Newtonsoft.Json;

namespace SEPTAInquirer.POCO
{
    // TODO: get to know more about the JSON.NET attributes
    public class SEPTANextToArriveAPIResult
    {
        // result is not ordered
        public IList<NextToArriveTrainTimeDto> TrainsToArriveAtHomeStation { get; set; }
    };


    /// <summary>
    /// Hold jsons object that look like
    /// {"orig_train":"568","orig_line":"Paoli\/Thorndale","orig_departure_time":" 3:01PM","orig_arrival_time":" 3:50PM","orig_delay":"On time","isdirect":"true"}
    /// </summary>
    public class NextToArriveTrainTimeDto
    {
        [JsonProperty("orig_departure_time")]
        public string OriginalDepartureTime { get; set; }

        [JsonProperty("orig_delay")]
        public string OriginalDelay { get; set; }
    }
}