using System;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    public abstract class XUnitWindowsServiceSpecification<TWebApplicationStarter> : WebApplicationSpecification<TWebApplicationStarter>, IDisposable
        where TWebApplicationStarter : class, IWebApplicationHostStarter, new()
    {
        private readonly TWebApplicationStarter _configuration;

        protected XUnitWindowsServiceSpecification()
        {
            _configuration = new TWebApplicationStarter();
            SetConfiguration(_configuration);
        }

        public void Dispose()
        {
            _configuration.Dispose();
            Client.Dispose();
        }
    }
}