using Owin;
using SimpleInjector;
using Test.It.Specifications;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using WebApi.Test.Application;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    internal class WebApiTestApplicationStarter<TApplication> : IWebApplicationStarter
        where TApplication : IApplication, new()
    {
        private readonly ITestConfigurer _testConfigurer;

        public WebApiTestApplicationStarter(ITestConfigurer testConfigurer)
        {
            _testConfigurer = testConfigurer;
        }

        public void Start(IAppBuilder applicationBuilder)
        {
            var testApplication = new TApplication();
            applicationBuilder.CatchExceptions(testApplication.HttpConfiguration);

            testApplication.Configure(applicationBuilder, Reconfigure);
        }

        private void Reconfigure(Container container)
        {
            _testConfigurer.Configure(new SimpleInjectorServiceContainer(container));
        }
    }
}