using System;
using System.Net.Http;
using Test.It.Specifications;

namespace Test.It.While.Hosting.Your.Web.Application.HostStarters
{
    public interface IWebApplicationHostStarter : IDisposable
    {
        HttpClient Start(ITestConfigurer testConfigurer);
        event ExceptionHandler OnUnhandledException;
    }
}