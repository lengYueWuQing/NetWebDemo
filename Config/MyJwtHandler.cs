using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Furion;
using Furion.Authorization;
using Furion.DataEncryption;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;


namespace WebApplication.Config
{
    public class MyJwtHandler : AppAuthorizeHandler
    {
        /// <summary>
        /// 重写 Handler 添加自动刷新收取逻辑
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task HandleAsync(AuthorizationHandlerContext context)
        {
            // 第三方授权自定义
            /*if (!isAuthenticated)
            {
                foreach (var requirement in pendingRequirements)
                {
                    // 授权成功
                    context.Succeed(requirement);
                }
            }
            // 授权失败
            else context.Fail();*/

            // 自动刷新 token
            if (MyAutoRefreshToken(context, context.GetCurrentHttpContext()))
            {
                await AuthorizeHandleAsync(context);
            }
            else context.Fail();    // 授权失败
        }

        /// <summary>
        /// 请求管道
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {
            // 此处已经自动验证 Jwt token的有效性了，无需手动验证

            // 检查权限，如果方法时异步的就不用 Task.FromResult 包裹，直接使用 async/await 即可
            return Task.FromResult(CheckAuthorzie(httpContext));
        }


        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static bool CheckAuthorzie(DefaultHttpContext httpContext)
        {
            // 获取权限特性
            //var securityDefineAttribute = httpContext.GetMetadata<SecurityDefineAttribute>();
            //if (securityDefineAttribute == null) return true;

            //return App.GetService<IAuthorizationManager>().CheckSecurity(securityDefineAttribute.ResourceId);
            return true;
        }

        /// <summary>
        /// 自动刷新 Token 信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContext"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static bool MyAutoRefreshToken(AuthorizationHandlerContext context, DefaultHttpContext httpContext, int days = 30)
        {
            // 如果验证有效，则跳过刷新
            if (context.User.Identity.IsAuthenticated) return true;

            // 判断是否含有匿名特性
            if (httpContext.GetEndpoint()?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null) return true;

            HttpRequest httpRequest = context.GetCurrentHttpContext().Request;
            IRequestCookieCollection cookies = httpRequest.Cookies;
            IHeaderDictionary headers = httpRequest.Headers;
            string expiredToken = null;
            string refreshToken = null;
            bool cookieFlag = false;
            if (cookies.ContainsKey("X-Authorization") && cookies.ContainsKey("Authorization"))
            {
                cookieFlag = true;
                expiredToken = cookies["Authorization"];
                refreshToken = cookies["X-Authorization"];
            }
            else if(headers.ContainsKey("X-Authorization") && headers.ContainsKey("Authorization")){
                expiredToken = headers["Authorization"];
                refreshToken = headers["X-Authorization"];
            }
            if (string.IsNullOrWhiteSpace(expiredToken) || string.IsNullOrWhiteSpace(refreshToken)) return false;
            // 原Token 未过期
            var (_isValid, _) = JWTEncryption.Validate(expiredToken);
            if (_isValid) return true;
            JWTSettingsOptions jwtOption = JWTEncryption.GetJWTSettings();
            // 交换新的 Token
            var accessToken = JWTEncryption.Exchange(expiredToken, refreshToken, jwtOption.ExpiredTime, jwtOption.ClockSkew != null? jwtOption.ClockSkew.Value : 5);
            if (string.IsNullOrWhiteSpace(accessToken)) return false;

            // 读取新的 Token Clamis
            var claims = JWTEncryption.ReadJwtToken(accessToken)?.Claims;
            if (claims == null) return false;

            // 创建身份信息
            var claimIdentity = new ClaimsIdentity("AuthenticationTypes.Federation");
            claimIdentity.AddClaims(claims);
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

            // 设置 HttpContext.User 并登录
            httpContext.User = claimsPrincipal;
            httpContext.SignInAsync(claimsPrincipal);

            // 返回新的 Token
            httpContext.Response.Headers["Authorization"] = accessToken;
            // 返回新的 刷新Token
            var accessRefreshToken = JWTEncryption.GenerateRefreshToken(accessToken, days);
            httpContext.Response.Headers["X-Authorization"] = accessRefreshToken;
            if (cookieFlag) {
                httpContext.Response.Cookies.Delete("Authorization");
                httpContext.Response.Cookies.Append("Authorization", accessToken);
                httpContext.Response.Cookies.Delete("X-Authorization");
                httpContext.Response.Cookies.Append("X-Authorization", accessRefreshToken);
            }

            return true;
        }

    }


}
