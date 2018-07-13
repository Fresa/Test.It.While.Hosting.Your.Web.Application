using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Test.It.While.Hosting.Your.Web.Application.Utils.Services;

namespace WebApi.Core.Test.Application
{
    public class ExceptionThrowingDuringCallApplication : IStartup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IService, SupermanService>();
            services.AddTransient<IMaidService, ConsuelasService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            return services.BuildServiceProvider(validateScopes: true);
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
