using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            _logger.LogError("异常: {0}", context.Exception);
            if (context.ExceptionHandled == false) {
                // 设置结果
                context.Result = new JsonResult(new UnifyResResult
                {
                    status = UnifyResResult.OK,
                    errors = App.Configuration["FriendlyExceptionSettings:DefaultErrorMessage"],
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
