using CoreProfiler;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace dotNetCore31.WebApi.Infrastructure.ActionFilters
{
    /// <summary>
    /// CoreProfiler Action Filter
    /// </summary>
    /// <seealso cref="System.Attribute" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IAsyncActionFilter" />
    public class CoreProfilerActionFilter : Attribute, IAsyncActionFilter
    {
        /// <summary>
        /// 在執行Action外面包一層，用來監控效能的CoreProfiler
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        /// <param name="next">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutionDelegate" />. Invoked to execute the next action filter or the action itself.</param>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (ProfilingSession.Current.Step(ProfilingSession.Current.Profiler.GetTimingSession().Name))
            {
                await next();
            }
        }
    }
}