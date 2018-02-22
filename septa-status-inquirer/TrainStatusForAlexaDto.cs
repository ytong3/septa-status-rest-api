using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SEPTAInquierierForAlexa
{
    public class TrainStatusForAlexaDto
    {
        public string TrainNumber { get; set; }
        public int LateInMinutes {get; set; }
    }
}
