using System.Threading.Tasks;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using Xunit;

namespace Test.It.While.Hosting.Your.Web.Application.Utils
{
    public abstract class XUnitWindowsServiceSpecificationAsync<TWebApplicationStarter> : WebApplicationSpecificationAsync<TWebApplicationStarter>, IAsyncLifetime
        where TWebApplicationStarter : class, IWebApplicationHostStarter, new()
    {
        private TWebApplicationStarter _configuration;

        protected async Task SetConfiguration()
        {
            _configuration = new TWebApplicationStarter();
            await SetConfiguration(_configuration);
        }

        protected override void Dispose(bool disposing)
        {
            _configuration?.Dispose();
            base.Dispose(disposing);
        }

        public virtual async Task InitializeAsync() => await SetConfiguration();

        public Task DisposeAsync() => Task.CompletedTask;
    }
}