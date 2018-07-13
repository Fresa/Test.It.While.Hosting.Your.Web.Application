using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return new SimpleInjectorServiceProvider(container, services.BuildServiceProvider(validateScopes: true));
        }

        public void Configure(IApplicationBuilder app)
        {
            Configure(app, new HostingEnvironment());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}