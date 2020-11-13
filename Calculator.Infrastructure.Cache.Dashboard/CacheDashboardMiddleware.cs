using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Calculator.Infrastructure.Cache.Dashboard
{
    public class CacheDashboardMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}
