using System;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    public abstract class XUnitWindowsServiceSpecification<TWebApplicationStarter> : WebApplicationSpecification<TWebApplicationStarter>, IDisposable
        where TWebApplicationStarter : class, IWebApplicationHostStarter, new()
    {
        private readonly TWebApplicationStarter _configuration;
        private bool _setupStarted;

        protected XUnitWindowsServiceSpecification(bool startSetup = true)
        {
            _configuration = new TWebApplicationStarter();
            if (startSetup)
            {
                SetConfiguration(_configuration);
            }

            _setupStarted = startSetup;
        }

        protected void SetConfiguration()
        {
            if (_setupStarted)
            {
                return;
            }

            SetConfiguration(_configuration);
            _setupStarted = true;
        }

        public void Dispose()
        {
            _configuration.Dispose();
            Client.Dispose();
        }
    }
}