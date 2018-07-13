using Owin;
using SimpleInjector;
using Test.It.Specifications;
using Test.It.While.Hosting.Your.Web.Application;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using Test.It.While.Hosting.Your.Web.Application.Utils;

namespace WebApi.Test.Application
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
            var exceptionHandler = applicationBuilder.GetTestExceptionHandler();
            testApplication.HttpConfiguration.InterceptExceptionHandler(exceptionHandler);
            
            testApplication.Configuration(applicationBuilder, Reconfigure);
        }

        private void Reconfigure(Container container)
        {
            _testConfigurer.Configure(new SimpleInjectorServiceContainer(container));
        }
    }
}