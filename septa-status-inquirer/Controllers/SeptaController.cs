using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEPTAInquierierForAlexa;

namespace SEPTAInquirer.Controllers
{
    [Route("api/SPETA")]
    public class SeptaController : Controller
    {
        private ISeptapiClient _septaApiClient;

        public SeptaController(ISeptapiClient septaApiclient)
        {
            _septaApiClient = septaApiclient;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
