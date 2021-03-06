﻿using System;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SEPTAInquirer.POCO;
using System.Collections.Generic;
using SEPTAInquirer.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors;

namespace SEPTAInquirer.Controllers
{
    [Produces("application/json")]
    [Route("api/alexa")]
    [EnableCors("CorsPolicy")]
    public class AlexaController : Controller
    {
        private AlexaSkillConfig _config;
        private readonly string _appid;
        private ISeptapiClient _septaClient;
        private ISeptaSpeechGenerator _speechGenerator;

        public AlexaController(IOptions<AlexaSkillConfig> config,
                               ISeptapiClient septaApiClient,
                               ISeptaSpeechGenerator speechGenerator)
        {
            _config = config.Value;
            _appid = _config.SkillAppId;
            _septaClient = septaApiClient;
            _speechGenerator = speechGenerator;
        }

        [HttpPost]
        public IActionResult HandleSkillRequest([FromBody]SkillRequest skillRequest)
        {
            try{
            // Security check
            if (skillRequest.Session.Application.ApplicationId != _appid)
            {
                return BadRequest();
            }

            var requestType = skillRequest.GetRequestType();
            if (requestType == typeof(IntentRequest))
            {
                var response = HandleIntents(skillRequest);
                return Ok(response);
            }
            else if (requestType == typeof(LaunchRequest))
            {
                var response = HandleIntents(skillRequest);
                return Ok(response);
            }
            else if (requestType == typeof(AudioPlayerRequest))
            {
                var speech = new SsmlOutputSpeech();
                speech.Ssml = "<speak>Audio player repsonse</speak>";
                var finalResponse = ResponseBuilder.Tell(speech);
                return Ok(finalResponse);
            }
            return Ok(ErrorResponse());
            }
            catch (Exception){
                return Ok(ErrorResponse());
            }

        }

        /// <summary>
        /// this handler is for making sure the controller is reachable.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetForTest(){
            return Ok("Yes, the controller is reachable.");
        }

        private SkillResponse ErrorResponse()
        {
            var speech = new SsmlOutputSpeech();
            speech.Ssml = "<speak>I'm sorry, something went wrong.</speak>";
            return ResponseBuilder.Tell(speech);
        }

        private SkillResponse HandleIntents(SkillRequest skillRequest)
        {
            var intentRequest = skillRequest.Request as IntentRequest;
            var speech = new SsmlOutputSpeech();

            if (intentRequest == null)
            {
                return ErrorResponse();
            }

            if (intentRequest.Intent.Name.Equals("AmILateIntent"))
            {
                var speechToSay  = HandleAmILateIntent();

                speech.Ssml = "<speak>"+speechToSay+"</speak>";
                return ResponseBuilder.Tell(speech);
            }

            return ErrorResponse();
        }

        private string HandleAmILateIntent()
        {
            var apiResult = _septaClient.GetNextToArriveFromHomeToDestinationAsync().GetAwaiter().GetResult();
            
            // the trains in apiResult may not have been ordered by the original departure time
            // sort apiResult before iterating.

            var nextToArriveList = new List<TrainInfo>();

            foreach (var trainInApiResult in apiResult)
            {
                var temp = new TrainInfo();
                temp.MapFrom(trainInApiResult);
                nextToArriveList.Add(temp);
            }

            return _speechGenerator.GenerateSpeechForAlexa(nextToArriveList, DateTime.UtcNow);
        }
    }
}