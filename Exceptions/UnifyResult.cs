using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion;
using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApplication.Exceptions
{
    public class UnifyResResult
    {
        public static int OK = 1;
        public static int NO = 0;
        public int status { get; set; }
        public object data { get; set; }
        public object errors { get; set; }
        public long timestamp { get; set; }
       
        public UnifyResResult()
        {

        }

        public static UnifyResResult success()
        {
            UnifyResResult res = new UnifyResResult();
            res.data = null;
            res.status = UnifyResResult.OK;
            res.errors = null;
            res.timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            return res;
        }

        public static UnifyResResult success(Object data)
        {
            UnifyResResult res = new UnifyResResult();
            res.data = data;
            res.status = OK;
            res.errors = null;
            res.timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            return res;
        }

        public static UnifyResResult failure(object errors)
        {
            UnifyResResult res = new UnifyResResult();
            res.data = null;
            res.status = UnifyResResult.NO;
            res.errors = errors;
            res.timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            return res;
        }

        public static UnifyResResult failure()
        {
            UnifyResResult res = new UnifyResResult();
            res.data = null;
            res.status = UnifyResResult.NO;
            res.errors = null;
            res.timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            return res;
        }

    }

    /// <summary>
    /// RESTful 风格返回值
    /// </summary>
    [SkipScan, UnifyModel(typeof(RESTfulResult<>))]
    public class RESTfulResultProvider : IUnifyResultProvider
    {
        /// <summary>
        /// 异常返回值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IActionResult OnException(ExceptionContext context)
        {
            // 解析异常信息
            var (StatusCode, ErrorCode, Errors) = UnifyContext.GetExceptionMetadata(context);

            return new JsonResult(new UnifyResResult
            {
                status = UnifyResResult.NO,
                errors = App.Configuration["FriendlyExceptionSettings:DefaultErrorMessage"],
                data = null,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                //Extras = UnifyContext.Take(),
            });
        }

        /// <summary>
        /// 成功返回值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IActionResult OnSucceeded(ActionExecutedContext context)
        {
            object data;
            // 处理内容结果
            if (context.Result is ContentResult contentResult) data = contentResult.Content;
            // 处理对象结果
            else if (context.Result is ObjectResult objectResult) data = objectResult.Value;
            else if (context.Result is EmptyResult) data = null;
            else return null;

            return new JsonResult(new UnifyResResult
            {
                status = UnifyResResult.OK,
                errors = null,
                data = data,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                //Extras = UnifyContext.Take(),
            });
        }

        /// <summary>
        /// 验证失败返回值
        /// </summary>
        /// <param name="context"></param>
        /// <param name="modelStates"></param>
        /// <param name="validationResults"></param>
        /// <param name="validateFailedMessage"></param>
        /// <returns></returns>
        public IActionResult OnValidateFailed(ActionExecutingContext context, ModelStateDictionary modelStates, IEnumerable<ValidateFailedModel> validationResults, string validateFailedMessage)
        {
            return new JsonResult(new UnifyResResult
            {
                status = UnifyResResult.NO,
                data = null,
                errors = validationResults,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                //Extras = UnifyContext.Take(),
            });
        }

        /// <summary>
        /// 处理输出状态码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task OnResponseStatusCodes(HttpContext context, int statusCode, UnifyResultStatusCodesOptions options)
        {
            // 设置响应状态码
            UnifyContext.SetResponseStatusCodes(context, statusCode, options);

            switch (statusCode)
            {
                // 处理 401 状态码
                case StatusCodes.Status401Unauthorized:
                    await context.Response.WriteAsJsonAsync(new UnifyResResult
                    {
                        status = UnifyResResult.NO,
                        errors = "未登录",
                        data = null,
                        timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                    }, App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                    break;
                // 处理 403 状态码
                case StatusCodes.Status403Forbidden:
                    await context.Response.WriteAsJsonAsync(new UnifyResResult
                    {
                        status = UnifyResResult.NO,
                        errors = "未认证",
                        data = null,
                        timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
                    }, App.GetOptions<JsonOptions>()?.JsonSerializerOptions);
                    break;

                default:
                    break;
            }
        }
    }
}
