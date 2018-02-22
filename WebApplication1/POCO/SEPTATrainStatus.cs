using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEPTAInquirer.POCO
{
    /// <summary>
    /// DTOs representing the json of http://www3.septa.org/hackathon/Arrivals/90506/5/
    /// </summary>
    public class SEPTATrainStatus
    {
        //TODO: change the type to a enum?
        public string Direction { get; set; }
        public string Path { get; set; }
        public string Train_id { get; set; }
        public string Status { get; set; }
        public string Next_Station {get; set; }
    }
}
