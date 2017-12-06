using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace URFU_MVC_1.Middlewares
{
    public class TimingMiddleware
    {
        private readonly RequestDelegate _next;

        public TimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            await _next(context);
            sw.Stop();
            await context.Response.WriteAsync(
                $"<!-- (Class) Elapsed {sw.ElapsedMilliseconds} ms -->"
            );
        }
    }
}
