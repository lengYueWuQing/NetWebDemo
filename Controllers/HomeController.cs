using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Furion;
using Furion.Authorization;
using Furion.DataEncryption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;
using WebApplication.Config;
using WebApplication.Entitys;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly TestServices _testServices;
		private readonly AppInfoOptions _options;
		private readonly ISqlSugarRepository repository; // 仓储对象：封装简单的CRUD
		private readonly SqlSugarClient db; // 核心对象：拥有完整的SqlSugar全部功能
		public HomeController(ILogger<HomeController> logger, TestServices testServices, IOptionsMonitor<AppInfoOptions> optionsMonitor
			, ISqlSugarRepository sqlSugarRepository)
		{
			_logger = logger;
			_testServices = testServices;
			_options = optionsMonitor.CurrentValue;
		}

		public IActionResult Index()
		{
			//ViewData["datas"] = _testServices.getHello();
			string mess = _options.Company+ _options.Name+ _options.Version;
			ViewData["datas"] = mess;
			return View();
		}

		public IActionResult Privacy()
		{
			int total = 0;
			//db.Queryable<AIS_MockBuildingInRoute>().Select(c => c.BuildingID > 1).ToPagedList(1, 2, ref total);
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult getHelloIndex()
		{
			ViewData["datas"] = _testServices.getHello();
			//return View("~/Home/Index");
			return Content("ss");
		}

		[HttpGet]
		[NonUnify, AllowAnonymous]
		public IActionResult loginIn(string userName, string passWord)
		{
			var datetimeOffset = DateTimeOffset.UtcNow;
			JWTSettingsOptions jwtSettings = JWTEncryption.GetJWTSettings();
			var accessToken = JWTEncryption.Encrypt(jwtSettings.IssuerSigningKey, new Dictionary<string, object>()
			{
				{ "UserId", 11 },  // 存储Id
                { "Account",userName }, // 存储用户名
				{ JwtRegisteredClaimNames.Iat, datetimeOffset.ToUnixTimeSeconds() },
				{ JwtRegisteredClaimNames.Nbf, datetimeOffset.ToUnixTimeSeconds() },
				{ JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddSeconds(jwtSettings.ExpiredTime.Value*60).ToUnixTimeSeconds() },
				{ JwtRegisteredClaimNames.Iss, jwtSettings.ValidIssuer},
				{ JwtRegisteredClaimNames.Aud, jwtSettings.ValidAudience }
			});

			// 获取刷新 token
			var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken, 30); // 第二个参数是刷新 token 的有效期，默认三十天

			// 设置请求报文头
			HttpContext.Response.Headers["Authorization"] = accessToken;
			HttpContext.Response.Headers["X-Authorization"] = refreshToken;
			HttpContext.Response.Cookies.Delete("Authorization");
			HttpContext.Response.Cookies.Append("Authorization", accessToken);
			HttpContext.Response.Cookies.Delete("X-Authorization");
			HttpContext.Response.Cookies.Append("X-Authorization", refreshToken);
			//return View("~/Home/Index");
			return Content("ok");
		}

		[HttpGet, AllowAnonymous]
		public IActionResult loginOut()
		{
			if (HttpContext.Response.Headers.ContainsKey("Authorization")) {
				HttpContext.Response.Headers.Remove("Authorization");
			}
			if (HttpContext.Response.Headers.ContainsKey("X-Authorization"))
			{
				HttpContext.Response.Headers.Remove("X-Authorization");
			}
			HttpContext.Response.Cookies.Delete("Authorization");
			HttpContext.Response.Cookies.Delete("X-Authorization");

			//return View("~/Home/Index");
			return Content("ok");
		}
	}
}
