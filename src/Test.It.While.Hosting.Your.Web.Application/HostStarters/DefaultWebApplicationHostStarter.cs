using System.Net.Http;
using Microsoft.Owin.Testing;
using Owin;
using Test.It.Specifications;
using Test.It.While.Hosting.Your.Web.Application.ApplicationBuilders;

namespace Test.It.While.Hosting.Your.Web.Application.HostStarters
{
    public class DefaultWebApplicationHostStarter<TApplicationBuilder> : IWebApplicationHostStarter
        where TApplicationBuilder : IWebApplicationBuilder, new()
    {
        private TestServer _testServer;

        public HttpClient Start(ITestConfigurer testConfigurer)
        {
            void Startup(IAppBuilder appBuilder)
            {
                appBuilder.Properties[OwinProperties.ExceptionHandler] = OnUnhandledException;

                var applicationBuilder = new TApplicationBuilder();
                applicationBuilder.CreateWith(testConfigurer).Start(appBuilder);
            }

            _testServer = TestServer.Create(Startup);

            return _testServer.HttpClient;
        }

        public event ExceptionHandler OnUnhandledException;

        public void Dispose()
        {
            _testServer?.Dispose();
        }
    }
}