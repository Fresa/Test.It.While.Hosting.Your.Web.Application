using Test.It.Specifications;
using Test.It.Starters;
using Test.It.While.Hosting.Your.Web.Application.ApplicationBuilders;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    public class WebApiTestApplicationBuilder : IApplicationBuilder
    {
        public IApplicationStarter CreateWith(ITestConfigurer configurer)
        {
            return new WebApiTestApplicationStarter(configurer);
        }
    }
}
