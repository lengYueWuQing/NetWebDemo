using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Config;

namespace WebApplication
{
	//配置文件转对象注册
	[AppStartup(99)]
	public class OptionsStartup:AppStartup
    {
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddConfigurableOptions<AppInfoOptions>()
			;
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			
		}
	}
}
