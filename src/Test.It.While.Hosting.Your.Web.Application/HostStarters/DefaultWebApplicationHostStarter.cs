using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Web.Application.HostStarters
{
    public class DefaultWebApplicationHostStarter<TApplication> : IWebApplicationHostStarter
        where TApplication : class, IStartup
    {
        private TestServer _testServer;

        public HttpClient Start(ITestConfigurer testConfigurer)
        {
            if (_testServer == null)
            {
                var webHostBuilder = new WebHostBuilder()
                    .UseStartup<TApplication>()
                    .ConfigureServices(services =>
                    {
                        testConfigurer.Configure(new WebApiServiceContainer(services));
                    });

                _testServer = new TestServer(webHostBuilder);
            }

            return _testServer.CreateClient();
        }

        public event ExceptionHandler OnUnhandledException;

        public void Dispose()
        {
            _testServer?.Dispose();
        }
    }
}