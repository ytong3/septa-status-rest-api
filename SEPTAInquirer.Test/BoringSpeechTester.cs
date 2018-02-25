using System;
using System.Collections.Generic;
using SEPTAInquirer.POCO;
using Xunit;

namespace SEPTAInquirer.Test
{
    //TODO: why choose xunit over ms test
    public class BoringSpeechTester
    {
        [Fact]
        public void WhenIsLateForTrain_ShouldGenerateExpectedSpeech()
        {
            //Given
            IList<NextToArriveTrainTimeDto> apiReturnedResult = new List<NextToArriveTrainTimeDto>()
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
            };

            var arrivingTrainList = new List<TrainInfo>();

            foreach (var trainsDto in apiReturnedResult)
            {
                var tmp = new TrainInfo();
                tmp.MapFrom(trainsDto);
                arrivingTrainList.Add(tmp);
            }
        //When
        var speechGenerator = new SpetaSpeechGenerator(new BoringAlexaSpeakStrategy());
        //Then
                    var speech = speechGenerator.GenerateSpeechForAlexa(arrivingTrainList, DateTime.Parse("5:13PM"));

            var expectedSpeech = "5:15 Train is delayed and is leaving in 5 minutes." +
                                 " You are not going to make it. Consider taking the next one." +
                                 " 5:25 Train is ontime and is leaving in 12 minutes.";


            Assert.Equal(expectedSpeech, speech);
        }
    }
}
