using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Furion;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using WebApplication.Config;
using WebApplication.Exceptions;

namespace WebApplication
{
	[AppStartup(100)]
	public class MyStartup : AppStartup
	{
		public MyStartup()
		{
		}
		public MyStartup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCorsAccessor();
			services.AddJwt<MyJwtHandler>(enableGlobalAuthorize: true);
			services.AddControllersWithViews(options =>
			{
				options.Filters.Add(new Filter.LogAsyncActionFilter());
			}).AddInjectWithUnifyResult<RESTfulResultProvider>().AddJsonOptions(options =>
			{
				//json 与对象配置  同意时间格式   字符编码
				options.JsonSerializerOptions.Converters.AddDateFormatString("yyyy-MM-dd HH:mm:ss");
				options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
				//options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
			});
			
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			//  NGINX 反向代理获取真实IP
			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSerilogRequestLogging();    // 必须在 UseStaticFiles 和 UseRouting 之间  Serilog注册
			app.UseRouting();
			app.UseCorsAccessor();  //跨域
			app.UseAuthorization();

			app.UseInject();
			
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
