using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Alexa.NET.Response.Directive;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace SEPTAInquirer.POCO
{
    public class TrainInfo
    {
        /// <summary>
        /// a train is identified with its original departure time. e.g. A train leaves Paoli at 8:02, is called the eight o' two train.
        /// </summary>
        public string TrainName { get; set; } = "Unknown";

        public int LateInMinutes { get; set; } = 0;
        public DateTime NowDeparureTime { get; set; }
        public TrainStatusEnum TrainStatus { get; set; }
    }
}

