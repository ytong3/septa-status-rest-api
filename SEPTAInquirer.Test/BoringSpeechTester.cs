using System;
using System.Collections.Generic;
using SEPTAInquirer.POCO;
using Xunit;

namespace SEPTAInquirer.Test
{
    public class BoringSpeechTester
    {
        [Fact]
        public void WhenIsLateForTrain_ShouldGenerateExpectedSpeech()
        {
        //Given
                    SEPTANextToArriveAPIResult apiReturnedResult = new SEPTANextToArriveAPIResult()
            {
                TrainsToArriveAtHomeStation = new List<NextToArriveTrainTimeDto>()
                {
                    new NextToArriveTrainTimeDto()
                    {
                        OriginalDelay = "On Time",
                        OriginalDepartureTime = "5:25PM"
                    },
                    new NextToArriveTrainTimeDto()
                    {
                        OriginalDelay = "3 mins",
                        OriginalDepartureTime = "5:15PM"
                    }
                }
            };

            var arrivingTrainList = new List<TrainInfo>();

            foreach (var trainsDto in apiReturnedResult.TrainsToArriveAtHomeStation)
            {
                var tmp = new TrainInfo();
                tmp.MapFrom(trainsDto);
                arrivingTrainList.Add(tmp);
            }
        //When
        var speechGenerator = new SpetaSpeechGenerator(new BoringAlexaSpeakStrategy());
        //Then
                    var speech = speechGenerator.GenerateSpeechForAlexa(arrivingTrainList, DateTime.Parse("5:13"));

            var expectedSpeech = "5:15 Train is delayed and is leaving in 5 minutes." +
                                 " You can make it if you leave now.";

            Assert.True(string.Equals(expectedSpeech, speech));
        }
    }
}
