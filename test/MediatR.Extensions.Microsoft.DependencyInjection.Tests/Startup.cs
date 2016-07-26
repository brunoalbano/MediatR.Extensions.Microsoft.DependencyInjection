using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace MediatR.Extensions.Microsoft.DependencyInjection.Tests
{
    public class Startup
    {
        public Startup()
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddScoped<PongScopedDependency>();

            // Uncomment this to make MediatR to work with Asp.Net scope per request
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddScoped<SingleInstanceFactory>(p =>
            //{
            //    return t =>
            //    {
            //        var httpContextAccessor = p.GetRequiredService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
            //        return httpContextAccessor.HttpContext.RequestServices.GetRequiredService(t);
            //    };
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var mediator = context.RequestServices.GetRequiredService<IMediator>();
                return context.Response.WriteAsync("Scoped per request id: " + mediator.Send(new PingScopedDependency()).Id);
            });
        }
    }
}
