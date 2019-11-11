using System;
using System.Threading;
using System.Threading.Tasks;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Web.Application
{
    public interface IWebApplicationHost : IDisposable
    {
        Task<IServer> StartAsync(ITestConfigurer testConfigurer, CancellationToken cancellationToken = default);
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}