using Owin;
using Test.It.Specifications;
using Test.It.Starters;
using WebApi.Test.Application;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    internal class WebApiTestApplicationStarter : IApplicationStarter
    {
        private readonly ITestConfigurer _testConfigurer;

        public WebApiTestApplicationStarter(ITestConfigurer testConfigurer)
        {
            _testConfigurer = testConfigurer;
        }

        public void Start(IAppBuilder applicationBuilder)
        {
            var testApplication = new TestApplication(_testConfigurer);
            testApplication.Configuration(applicationBuilder);
        }
    }
}