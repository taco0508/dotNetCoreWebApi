using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace dotNetCore31.WebApi.Infrastructure.Middlewares
{
    /// <summary>
    /// 針對客戶端要求訪問的網址來做限制 Middleware 擴充方法
    /// </summary>
    public static class RequestFilterMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestFilter(this IApplicationBuilder builder)
        {
            var options = new RequestFilterOptions
            {
                WhiteListIpCollection = DefaultWhiteListIpCollection,
                RestrictPathCollection = DefaultRestrictPathCollection
            };
            return builder.UseMiddleware<RequestFilterMiddleware>(options);
        }

        public static IApplicationBuilder UseRequestFilter(
            this IApplicationBuilder builder, 
            Action<RequestFilterOptions> requestFilterOptions)
        {
            var options = new RequestFilterOptions
            {
                WhiteListIpCollection = DefaultWhiteListIpCollection,
                RestrictPathCollection = DefaultRestrictPathCollection
            };
            requestFilterOptions(options);
            return builder.UseMiddleware<RequestFilterMiddleware>(options);
        }

        /// <summary>
        /// 預設的 IP 白名單
        /// </summary>
        private static IEnumerable<string> DefaultWhiteListIpCollection => new[]
        {
            "::1",
            "127.",
            "192.",
            "10.",
            "172."
        };

        /// <summary>
        /// 預設的限制路徑名稱
        /// </summary>
        private static IEnumerable<string> DefaultRestrictPathCollection => new[]
        {
            "coreprofiler",
            "nanoprofiler",
            "swagger",
            "health"
        };
    }
}
