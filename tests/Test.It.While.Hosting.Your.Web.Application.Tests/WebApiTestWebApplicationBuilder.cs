using Test.It.Specifications;
using Test.It.Starters;
using Test.It.While.Hosting.Your.Web.Application.ApplicationBuilders;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using WebApi.Test.Application;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    public class WebApiTestWebApplicationBuilder<TApplication> : IWebApplicationBuilder 
        where TApplication : IApplication, new()
    {
        public IWebApplicationStarter CreateWith(ITestConfigurer configurer)
        {
            return new WebApiTestApplicationStarter<TApplication>(configurer);
        }
    }
}
