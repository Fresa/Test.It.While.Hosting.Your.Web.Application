﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Core.Test.Application.Services;

namespace WebApi.Core.Test.Application
{
    public class ExceptionThrowingDuringCallApplication : IStartup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IService, SupermanService>();
            services.AddTransient<IMaidService, ConsuelasService>();

            services.AddMvc();
            return services.BuildServiceProvider(validateScopes: true);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
                {
                    // Do work that doesn't write to the Response.
                    await next.Invoke();
                    // Do logging or other work that doesn't write to the Response.
                })
                .UseRouting()
                .UseEndpoints(routeBuilder =>
                    routeBuilder.MapControllers());
        }
    }
}
