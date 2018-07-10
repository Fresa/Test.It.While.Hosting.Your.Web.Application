using Test.It.Specifications;
using Test.It.While.Hosting.Your.Web.Application.ApplicationBuilders;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;

namespace WebApi.Core.Test.Application
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
