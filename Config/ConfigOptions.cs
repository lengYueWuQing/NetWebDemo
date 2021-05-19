
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Furion.ConfigurableOptions;
using Microsoft.Extensions.Configuration;

namespace WebApplication.Config
{
    //配置项转对象 可以限制字段值  监听字段值变化
    public class AppInfoOptions : IConfigurableOptions<AppInfoOptions>
    {
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }
        public string Version { get; set; }
        [Required, MaxLength(100)]
        public string Company { get; set; }

        public void OnListener(AppInfoOptions options, IConfiguration configuration)
        {
            var name = options.Name;  // 实时的最新值
            var version = options.Version;  // 实时的最新值
        }

        //配置文件属性值为null时，获取到的值为 空 
        public void PostConfigure(AppInfoOptions options, IConfiguration configuration)
        {
            options.Name ??= "Furion";
            options.Version ??= "1.0.0";
            options.Company ??= "Baiqian";
        }
    }
}
