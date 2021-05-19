using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Furion.JsonSerialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Models;

namespace WebApplication.Controllers
{

    [Route("[controller]")]
    [ApiDescriptionSettings("MyGroup")]
    public class TestDynamicApiController: IDynamicApiController {

        private readonly ILogger<TestDynamicApiController> _logger;

        public TestDynamicApiController(ILogger<TestDynamicApiController> logger)
        {
            _logger = logger;
        }

        [HttpPost, HttpGet("get/[action]"), AcceptVerbs("PUT", "DELETE")]
        public string GetVersion()
        {
            return "1.0.0";
        }

        [HttpPost, NonUnify]
        [AllowAnonymous]
        //不支持 Content  直接写对象
        public Person GetPerson([FromBody] Person person)
        {
            //对象转json
            string str = JSON.Serialize(person);
            
            _logger.LogDebug("请求的值：{0}", str);
            var obj = JSON.Deserialize<Person>(str);
            return obj;
        }
    }
}
