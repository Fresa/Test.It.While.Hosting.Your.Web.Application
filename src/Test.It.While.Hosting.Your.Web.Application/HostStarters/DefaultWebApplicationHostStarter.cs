using System.Net.Http;
using Microsoft.Owin.Testing;
using Test.It.Specifications;
using Test.It.While.Hosting.Your.Web.Application.ApplicationBuilders;

namespace Test.It.While.Hosting.Your.Web.Application.HostStarters
{
    public class DefaultWebApplicationHostStarter<TApplicationBuilder> : IWebApplicationHostStarter 
        where TApplicationBuilder : IApplicationBuilder, new()
    {
        private TestServer _testServer;
        
        public HttpClient Start(ITestConfigurer testConfigurer)
        {
            var applicationBuilder = new TApplicationBuilder();
            _testServer = TestServer.Create(applicationBuilder.CreateWith(testConfigurer).Start);

            return _testServer.HttpClient;
        }

        public void Dispose()
        {
            _testServer.Dispose();
        }
    }
}