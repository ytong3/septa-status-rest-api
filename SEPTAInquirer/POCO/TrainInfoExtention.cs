using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace SEPTAInquirer.POCO
{
    public static class TrainInfoExtentions {
        static TimeZoneInfo _ESTTimeZone => (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time") : TimeZoneInfo.FindSystemTimeZoneById("EST"));

        public static void MapFrom(this TrainInfo trainInfo, NextToArriveTrainDto apiResult)
        {
            // sample source
            //{"orig_train":"568","orig_line":"Paoli\/Thorndale","orig_departure_time":" 3:01PM","orig_arrival_time":" 3:50PM","orig_delay":"On time","isdirect":"true"}
            //trainName

            //TODO: use something like https://github.com/Humanizr/Humanizer to make the speech more natural, like 09:04 translates to nine,o',four train
            trainInfo.TrainName = Regex.Match(apiResult.OriginalDepartureTime, @"[\d:]+").Value;

            //convert and use utc from now on.
            // convert utcNow to EST
            var utcNow = DateTime.UtcNow;
            var estToday = TimeZoneInfo.ConvertTimeFromUtc(utcNow, _ESTTimeZone);

            var departureTimeInEST =  DateTime.Parse($"{estToday.ToShortDateString()} {apiResult.OriginalDepartureTime}");
            
            trainInfo.NowDeparureTime = TimeZoneInfo.ConvertTimeToUtc(departureTimeInEST, _ESTTimeZone);

            if (string.Equals(apiResult.OriginalDelay, "On time", StringComparison.OrdinalIgnoreCase))
            {
                trainInfo.TrainStatus = TrainStatusEnum.OnTime;
            }
            else if (string.Equals(apiResult.OriginalDelay, "Canceled", StringComparison.OrdinalIgnoreCase))
            {
                trainInfo.TrainStatus = TrainStatusEnum.Cacneled;
            }
            else
            {
                trainInfo.TrainStatus = TrainStatusEnum.Delayed;
                trainInfo.LateInMinutes = Int32.Parse(Regex.Match(apiResult.OriginalDelay, @"\d+").Value);
                trainInfo.NowDeparureTime = trainInfo.NowDeparureTime.AddMinutes(trainInfo.LateInMinutes);
            }
        }
    }
}