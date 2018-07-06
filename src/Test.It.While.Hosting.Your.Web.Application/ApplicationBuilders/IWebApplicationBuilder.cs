using Test.It.Specifications;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;

namespace Test.It.While.Hosting.Your.Web.Application.ApplicationBuilders
{
    public interface IWebApplicationBuilder
    {
        IWebApplicationStarter CreateWith(ITestConfigurer configurer);
    }
}