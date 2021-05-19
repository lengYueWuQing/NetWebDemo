using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Furion.DependencyInjection;
using Furion.JsonSerialization;
using Furion.Logging.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using UAParser;

namespace WebApplication.Filter
{
    public class LogAsyncActionFilter : IAsyncActionFilter, ITransient
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            var httpRequest = httpContext.Request;

            var sw = new Stopwatch();
            sw.Start();
            var actionContext = await next();
            sw.Stop();

            // 判断是否请求成功（没有异常就是请求成功）
            var isRequestSucceed = actionContext.Exception == null;
            var headers = httpRequest.Headers;
            var clientInfo = headers.ContainsKey("User-Agent")
                ? Parser.GetDefault().Parse(headers["User-Agent"])
                : null;
            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            string logs = string.Format("Except:{0} ,IP:{1} ,URL:{2}, Browser:{3}, Os:{4}, Location:{5}, ClassName:{6}, MethodName:{7}, ReqMethod：{8}, Param:{9}, Result:{10}, ElapsedTime:{11}, OpTime:{12}",
               !isRequestSucceed, httpContext.GetRemoteIpAddressToIPv4(), httpRequest.Path, clientInfo?.UA.Family + clientInfo?.UA.Major, clientInfo?.OS.Family + clientInfo?.OS.Major, httpRequest.GetRequestUrlAddress(),
               context.Controller.ToString(), actionDescriptor?.ActionName, httpRequest.Method, JSON.Serialize(context.ActionArguments.Count < 1 ? "" : context.ActionArguments), actionContext.Result?.GetType() == typeof(JsonResult) ? JSON.Serialize(actionContext.Result) : "", sw.ElapsedMilliseconds, DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            logs.LogDebug<LogAsyncActionFilter>();

            /*MessageCenter.Send("create:oplog", new SysLogOp
            {
                Name = httpContext.User?.FindFirstValue(ClaimConst.CLAINM_NAME),
                Success = isRequestSucceed ? YesOrNot.Y : YesOrNot.N,
                Ip = httpContext.GetRemoteIpAddressToIPv4(),
                Location = httpRequest.GetRequestUrlAddress(),
                Browser = clientInfo?.UA.Family + clientInfo?.UA.Major,
                Os = clientInfo?.OS.Family + clientInfo?.OS.Major,
                Url = httpRequest.Path,
                ClassName = context.Controller.ToString(),
                MethodName = actionDescriptor?.ActionName,
                ReqMethod = httpRequest.Method,
                Param = JSON.Serialize(context.ActionArguments.Count < 1 ? "" : context.ActionArguments),
                Result = actionContext.Result?.GetType() == typeof(JsonResult) ? JSON.Serialize(actionContext.Result) : "",
                ElapsedTime = sw.ElapsedMilliseconds,
                OpTime = DateTimeOffset.Now,
                Account = httpContext.User?.FindFirstValue(ClaimConst.CLAINM_ACCOUNT)
            });*/
        }

    }

}
