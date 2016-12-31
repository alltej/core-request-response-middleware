using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core_request_response_middleware.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace core_request_response_middleware.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IOptions<AppSettingsConfig> _settings;
        public ValuesController(IOptions<AppSettingsConfig> settings)
        {
            _settings = settings;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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


        // GET api/values
        [HttpGet("app")]
        public IActionResult GetAppSettings()
        {
            return Ok(_settings.Value);
        }
    }
}
