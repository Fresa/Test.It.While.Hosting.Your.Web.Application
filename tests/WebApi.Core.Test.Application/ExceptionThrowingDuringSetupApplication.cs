using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using Test.It.While.Hosting.Your.Web.Application.Utils.Services;

namespace WebApi.Core.Test.Application
{
    public class ExceptionThrowingDuringSetupApplication : IStartup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var container = new Container();
            container.Register<IService, SupermanService>();

            container.Verify();

            services.AddMvc();

            return new SimpleInjectorServiceProvider(container, services.BuildServiceProvider(validateScopes: true));
        }

        public void Configure(IApplicationBuilder app)
        {
            app
                .UseRouting()
                .UseEndpoints(routeBuilder =>
                    routeBuilder.MapControllers());
        }
    }
}