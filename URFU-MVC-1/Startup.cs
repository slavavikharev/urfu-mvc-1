using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using URFU_MVC_1.Middlewares;
using URFU_MVC_1.Context;
using URFU_MVC_1.Repositories;
using URFU_MVC_1.Interfaces;

namespace URFU_MVC_1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IStudentsRepository, StudentsRepository>();

            services.AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<StudentAppContext>(
                        opt => opt.UseInMemoryDatabase()
                    );

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(async (context, next) => {
                var sw = new Stopwatch();
                sw.Start();
                await next.Invoke();
                sw.Stop();
                await context.Response.WriteAsync(
                    $"<!-- (Func) Elapsed {sw.ElapsedMilliseconds} ms -->"
                );
            });

            app.UseMiddleware<TimingMiddleware>();

            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
