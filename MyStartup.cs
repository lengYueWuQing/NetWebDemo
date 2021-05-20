using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Furion;
using Furion.Logging.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Serilog;
using SqlSugar;
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
			services.AddSqlSugar(new ConnectionConfig
			{
				ConnectionString = App.Configuration["ConnectionStrings:SqlServerConnectionString"],//连接符字串
				DbType = DbType.SqlServer,
				IsAutoCloseConnection = true,
				InitKeyType = InitKeyType.Attribute//从特性读取主键自增信息
			},
			db =>
			{
				//处理日志事务 sql执行前
				db.Aop.OnLogExecuting = (sql, pars) =>
				{
					//记录log
					string sqlStr = sql + System.Environment.NewLine + string.Join(",", pars?.Select(it => it.ParameterName + ":" + it.Value));
					sqlStr.LogDebug("LogShow");
				};
				db.Aop.OnError = (exp) =>//SQL报错
				{
					//记录错误log
					string sqlStr = exp.Sql + System.Environment.NewLine + exp.Parametres.ToString()+ System.Environment.NewLine+ exp.Message;
					sqlStr.LogDebug("LogShow");
				};
				db.Aop.OnExecutingChangeSql = (sql, pars) => //可以修改SQL和参数的值
				{
					return new KeyValuePair<string, SugarParameter[]>(sql, pars);
				};
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
			// 添加规范化结果状态码，需要在这里注册 （401 403 状态重写默认）
			app.UseUnifyResultStatusCodes();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
            // 配置模块化静态资源
            /*app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(""),
                RequestPath = "/login",  // 后续所有资源都是通过 /模块名称/xxx.css 调用
                EnableDirectoryBrowsing = true   //浏览器浏览到所有文件
            });*/
        }
	}
}
