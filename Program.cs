using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace WebApplication
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.Inject().UseStartup<Startup>();
				}).UseSerilogDefault(config =>//默认集成了 控制台 和 文件 方式。如需自定义写入，则传入需要写入的介质即可：
                {
                    string date = DateTime.Now.ToString("yyyy-MM-dd");//按时间创建文件夹
                    string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}";//输出模板

                    ///1.输出所有restrictedToMinimumLevel：LogEventLevel类型
                    config
                        //.MinimumLevel.Debug() // 所有Sink的最小记录级别
                        //.MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
                        //.Enrich.FromLogContext()
                        .WriteTo.Console(outputTemplate: outputTemplate)
                        .WriteTo.File($"_log/{date}/application.log",
                               outputTemplate: outputTemplate,
                                restrictedToMinimumLevel: LogEventLevel.Information,
                                rollingInterval: RollingInterval.Day,//日志按日保存，这样会在文件名称后自动加上日期后缀
                                                                     //rollOnFileSizeLimit: true,          // 限制单个文件的最大长度
                                                                     //retainedFileCountLimit: 10,         // 最大保存文件数,等于null时永远保留文件。
                                                                     //fileSizeLimitBytes: 10 * 1024,      // 最大单个文件大小
                                encoding: Encoding.UTF8            // 文件字符编码
                            )

                    #region 2.按LogEventLevel.输出独立发布/单文件

                    ///2.1仅输出 LogEventLevel.Debug 类型
                    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Debug)//筛选过滤
                        .WriteTo.File($"_log/{date}/{LogEventLevel.Debug}.log",
                            outputTemplate: outputTemplate,
                            rollingInterval: RollingInterval.Day,//日志按日保存，这样会在文件名称后自动加上日期后缀
                            encoding: Encoding.UTF8            // 文件字符编码
                         )
                                    )

                    ///2.2仅输出 LogEventLevel.Error 类型
                    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)//筛选过滤
                        .WriteTo.File($"_log/{date}/{LogEventLevel.Error}.log",
                            outputTemplate: outputTemplate,
                            rollingInterval: RollingInterval.Day,//日志按日保存，这样会在文件名称后自动加上日期后缀
                            encoding: Encoding.UTF8            // 文件字符编码
                         )
                                    )

                                #endregion 按LogEventLevel 独立发布/单文件

                                    ;
                            });
    }
}
