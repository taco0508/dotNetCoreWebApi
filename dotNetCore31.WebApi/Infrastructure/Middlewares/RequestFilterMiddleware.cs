using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace dotNetCore31.WebApi.Infrastructure.Middlewares
{
    /// <summary>
    /// 針對客戶端要求訪問的網址來做限制 Middleware
    /// </summary>
    public class RequestFilterMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly RequestFilterOptions _requestFilterOptions;

        public RequestFilterMiddleware(
            RequestDelegate next, 
            IHttpContextAccessor httpContextAccessor,
            RequestFilterOptions requestFilterOptions)
        {
            this._next = next;
            this._httpContextAccessor = httpContextAccessor;
            this._requestFilterOptions = requestFilterOptions;
        }

        /// <summary>
        /// 如果客戶端要求訪問的是限制的網址清單，就比對客戶端IP與白名單IP是否符合，
        /// 符合就放行，不符合就返回404
        /// </summary>
        /// <param name="context">The context.</param>
        public async Task Invoke(HttpContext context)
        {
            var requestPath = context.Request.Path.ToString();
            var isPathInRestricts = this._requestFilterOptions.RestrictPathCollection.Any(x => requestPath.StartsWith($"/{x}", StringComparison.OrdinalIgnoreCase));

            //如果客戶端要求訪問的網址符合限制的網址清單
            if (isPathInRestricts.Equals(true))
            {
                var ipAddress = context.Connection.RemoteIpAddress.ToString();
                var clientIp = this._httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

                ipAddress = string.IsNullOrWhiteSpace(clientIp)
                    ? ipAddress
                    : clientIp ?? "";

                var isIpInWhiteList = this._requestFilterOptions.WhiteListIpCollection.Any(x => ipAddress.StartsWith(x));
                if (isIpInWhiteList.Equals(false))
                {
                    // 當客戶端IP不在白名單IP裡時，就返回404
                    context.Response.StatusCode = 404;
                    return;
                }
            }

            await this._next.Invoke(context);
        }
    }

    /// <summary>
    /// Request Filter 參數設定檔
    /// </summary>
    public class RequestFilterOptions
    {
        /// <summary>
        /// IP 白名單
        /// </summary>
        public IEnumerable<string> WhiteListIpCollection { get; set; }

        /// <summary>
        /// 限制的網址清單
        /// </summary>
        public IEnumerable<string> RestrictPathCollection { get; set; }
    }
}
