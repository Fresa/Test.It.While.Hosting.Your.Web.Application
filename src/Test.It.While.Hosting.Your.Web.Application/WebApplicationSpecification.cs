using System.Net.Http;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;

namespace Test.It.While.Hosting.Your.Web.Application
{
    public abstract class WebApplicationSpecification<TWebApplicationHostStarter> : IUseConfiguration<TWebApplicationHostStarter> 
        where TWebApplicationHostStarter : IWebApplicationHostStarter, new()
    {
        protected HttpClient Client;

        public void SetConfiguration(TWebApplicationHostStarter webHostingFixture)
        {
            Client = webHostingFixture.Start(new SimpleTestConfigurer(Given));

            When();
        }

        protected virtual void Given(IServiceContainer configurer) { }
        protected virtual void When() { }
    }
}