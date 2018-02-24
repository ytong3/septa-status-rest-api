using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using SEPTAInquierierForAlexa;
using SEPTAInquirer.Controllers;
using SEPTAInquirer.POCO;

namespace SEPTAInquirer.Test
{
    public class AlexaController_Test
    {
        public void RecieveAmILateIntent_ShouldGenerateTheExpectedSpeech()
        {
            SEPTANextToArriveAPIResult apiReturnedResult = new SEPTANextToArriveAPIResult()
            {
                TrainsToArriveAtHomeStation = new List<NextToArriveTrainTimeDto>()
                {
                    new NextToArriveTrainTimeDto()
                    {
                        OriginalDelay = "On Time",
                        OriginalDepartureTime = "5:15PM"
                    }
                }
            };

            var septaAPIMock = new Mock<ISeptapiClient>();
            septaAPIMock.Setup(client => client.GetNextToArriveFromHomeToDestinationAsync())
                .Returns(Task.FromResult(apiReturnedResult));

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(config => config["AlexaSkillConfig:"]);


            //TODO: to be finished.
            //Controller alexController = new AlexaController();
        }
    }
}
