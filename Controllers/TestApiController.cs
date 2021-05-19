using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Services;

namespace WebApplication.Controllers
{
	[ApiController]
	[Route("test/[controller]")]
	public class TestApiController:ControllerBase
	{
		private readonly ILogger<TestApiController> _logger;

		private readonly TestServices _testServices;
		public TestApiController(ILogger<TestApiController> logger,TestServices testServices)
		{
			_logger = logger;
			_testServices = testServices;
		}

		[HttpGet]
		[Route("[action]")]
		public IActionResult getTest()
		{
			return Content(_testServices.getHello());
		}
		[HttpGet]
		[Route("[action]")]
		public IActionResult getHello()
		{
			return Content(_testServices.getHello());
		}

		
	}
}
