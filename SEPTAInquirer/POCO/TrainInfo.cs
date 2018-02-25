using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Alexa.NET.Response.Directive;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace SEPTAInquirer.POCO
{
    //TODO: finish the DTO by looking according to the requirement of Alexa doc
    public class TrainInfo
    {
        /// <summary>
        /// a train is identified with its original departure time. e.g. A train leaves Paoli at 8:02, is called the eight o' two train.
        /// </summary>
        public string TrainName { get; set; } = "Unknown";

        public int LateInMinutes { get; set; } = 0;
        public DateTime NowDeparureTime { get; set; }
        public TrainStatusEnum TrainStatus { get; set; }

        //TODO: differentiate creation from initialization and usage (the clean code book)
        public TrainInfo()
        {
            
        }

        public void MapFrom(NextToArriveTrainTimeDto apiDto)
        {
            //NOTE: sample source 
            //{"orig_train":"568","orig_line":"Paoli\/Thorndale","orig_departure_time":" 3:01PM","orig_arrival_time":" 3:50PM","orig_delay":"On time","isdirect":"true"}
            //trainName

            //TODO: use something like https://github.com/Humanizr/Humanizer to make the speech more natural, like 09:04 translates to nine,o',four train
            TrainName = Regex.Match(apiDto.OriginalDepartureTime, @"[\d:]+").Value;
            NowDeparureTime = DateTime.Parse(apiDto.OriginalDepartureTime);

            if (string.Equals(apiDto.OriginalDelay, "On time", StringComparison.OrdinalIgnoreCase))
            {
                TrainStatus = TrainStatusEnum.OnTime;
            }
            // TODO: actually not quite sure about this status
            else if (string.Equals(apiDto.OriginalDelay, "Canceled", StringComparison.OrdinalIgnoreCase))
            {
                TrainStatus = TrainStatusEnum.Cacneled;
            }
            else
            {
                TrainStatus = TrainStatusEnum.Delayed;
                LateInMinutes = Int32.Parse(Regex.Match(apiDto.OriginalDelay, @"\d+").Value);
                NowDeparureTime = NowDeparureTime.AddMinutes(LateInMinutes);
            }
        }
    }
}

