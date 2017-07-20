using Test.It.Specifications;
using Test.It.Starters;

namespace Test.It.While.Hosting.Your.Web.Application.ApplicationBuilders
{
    public interface IApplicationBuilder
    {
        IApplicationStarter CreateWith(ITestConfigurer configurer);
    }
}