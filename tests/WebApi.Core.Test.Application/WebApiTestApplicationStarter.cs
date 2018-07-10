using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SimpleInjector;
using Test.It.Specifications;
using Test.It.While.Hosting.Your.Web.Application;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using Test.It.While.Hosting.Your.Web.Application.Utils;

namespace WebApi.Core.Test.Application
{
    internal class WebApiTestApplicationStarter<TApplication> : IWebApplicationStarter
        where TApplication : IApplication, new()
    {
        private readonly ITestConfigurer _testConfigurer;

        public WebApiTestApplicationStarter(ITestConfigurer testConfigurer)
        {
            _testConfigurer = testConfigurer;
        }

        private void Reconfigure(Container container)
        {
            _testConfigurer.Configure(new SimpleInjectorServiceContainer(container));
        }

        public void Start(IApplicationBuilder applicationBuilder)
        {
            var testApplication = new TApplication();
            applicationBuilder.CatchExceptions();

            testApplication.Configure(applicationBuilder, Reconfigure);
        }
    }
}