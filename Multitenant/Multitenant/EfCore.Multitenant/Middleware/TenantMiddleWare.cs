using EfCore.Multitenant.Extensions;
using EfCore.Multitenant.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore.Multitenant.Middleware
{
    public class TenantMiddleWare
    {
        private readonly RequestDelegate _next;

        public TenantMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var tenant = httpContext.RequestServices.GetRequiredService<TenantData>();
            tenant.TenantId = httpContext.GetTenantId();
            await _next(httpContext);
        }
    }
}
