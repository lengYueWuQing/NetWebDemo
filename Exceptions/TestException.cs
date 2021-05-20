using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace WebApplication.Exceptions
{
    /// <summary>
    /// 全局异常
    /// </summary>
    public class TestException : IGlobalExceptionHandler, ISingleton
    {
        private readonly ILogger<TestException> _logger;
        public TestException(ILogger<TestException> logger)
        {
            _logger = logger;
        }

        
        public Task OnExceptionAsync(ExceptionContext context)
        {
            // 写日志
            string path = string.Empty;
            RouteValueDictionary values = context.RouteData.Values;
            foreach (object key in values.Keys) {
                path += @"["+key.ToString()+":"+ values[key.ToString()] + @"] ";
            }
            _logger.LogError(path + " 异常: {0}", context.Exception);
            if (context.ExceptionHandled == false) {
                string errors = App.Configuration["FriendlyExceptionSettings:DefaultErrorMessage"];
                String message = context.Exception.Message;
                //修改的数据不是最新版本
                if (message.Contains("SqlSugar.VersionExceptions") && message.Contains("Not the latest version")) {
                    errors = "修改的非最新数据，请刷新页面后再修改";
                }
                
                // 设置结果
                context.Result = new JsonResult(new UnifyResResult
                {
                    status = UnifyResResult.NO,
                    errors = errors,
                    data = null,
                    timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                    //Extras = UnifyContext.Take(),
                });
                context.ExceptionHandled = true;
            }

            return Task.CompletedTask;
        }
    }
}
