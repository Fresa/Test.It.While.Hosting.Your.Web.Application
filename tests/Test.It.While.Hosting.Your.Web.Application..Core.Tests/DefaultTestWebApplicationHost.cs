using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Web.Application.Utils
{
    public class DefaultTestWebApplicationHost<TApplication> : IWebApplicationHost
        where TApplication : class, IStartup
    {
        private TestServer _testServer;
        
        public async Task<IServer> StartAsync(
            ITestConfigurer testConfigurer, 
            CancellationToken cancellationToken = default)
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

                await _testServer.Host.StartAsync(cancellationToken);
            }

            return new TestServerWrapper(_testServer);
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            if (_testServer?.Host != null)
            {
                await _testServer.Host.StopAsync(cancellationToken);
            }
        }

        public void Dispose()
        {
            _testServer?.Dispose();
        }
    }
}