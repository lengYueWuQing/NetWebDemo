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
				}).UseSerilogDefault(config =>//Ĭ�ϼ����� ����̨ �� �ļ� ��ʽ�������Զ���д�룬������Ҫд��Ľ��ʼ��ɣ�
                {
                    string date = DateTime.Now.ToString("yyyy-MM-dd");//��ʱ�䴴���ļ���
                    string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}";//���ģ��

                    ///1.�������restrictedToMinimumLevel��LogEventLevel����
                    config
                        //.MinimumLevel.Debug() // ����Sink����С��¼����
                        //.MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
                        //.Enrich.FromLogContext()
                        .WriteTo.Console(outputTemplate: outputTemplate)
                        .WriteTo.File($"_log/{date}/application.log",
                               outputTemplate: outputTemplate,
                                restrictedToMinimumLevel: LogEventLevel.Information,
                                rollingInterval: RollingInterval.Day,//��־���ձ��棬���������ļ����ƺ��Զ��������ں�׺
                                                                     //rollOnFileSizeLimit: true,          // ���Ƶ����ļ�����󳤶�
                                                                     //retainedFileCountLimit: 10,         // ��󱣴��ļ���,����nullʱ��Զ�����ļ���
                                                                     //fileSizeLimitBytes: 10 * 1024,      // ��󵥸��ļ���С
                                encoding: Encoding.UTF8            // �ļ��ַ�����
                            )

                    #region 2.��LogEventLevel.�����������/���ļ�

                    ///2.1����� LogEventLevel.Debug ����
                    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Debug)//ɸѡ����
                        .WriteTo.File($"_log/{date}/{LogEventLevel.Debug}.log",
                            outputTemplate: outputTemplate,
                            rollingInterval: RollingInterval.Day,//��־���ձ��棬���������ļ����ƺ��Զ��������ں�׺
                            encoding: Encoding.UTF8            // �ļ��ַ�����
                         )
                                    )

                    ///2.2����� LogEventLevel.Error ����
                    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(evt => evt.Level == LogEventLevel.Error)//ɸѡ����
                        .WriteTo.File($"_log/{date}/{LogEventLevel.Error}.log",
                            outputTemplate: outputTemplate,
                            rollingInterval: RollingInterval.Day,//��־���ձ��棬���������ļ����ƺ��Զ��������ں�׺
                            encoding: Encoding.UTF8            // �ļ��ַ�����
                         )
                                    )

                                #endregion ��LogEventLevel ��������/���ļ�

                                    ;
                            });
    }
}
