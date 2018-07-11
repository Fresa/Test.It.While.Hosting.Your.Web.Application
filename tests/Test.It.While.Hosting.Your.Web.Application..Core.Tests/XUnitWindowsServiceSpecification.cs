using System.Threading.Tasks;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using Xunit;

namespace Test.It.While.Hosting.Your.Web.Application.Utils
{
    public abstract class XUnitWindowsServiceSpecification<TWebApplicationStarter> : WebApplicationSpecification<TWebApplicationStarter>, IAsyncLifetime
        where TWebApplicationStarter : class, IWebApplicationHostStarter, new()
    {
        private TWebApplicationStarter _configuration;

        protected async Task SetConfigurationAsync()
        {
            _configuration = new TWebApplicationStarter();
            await SetConfigurationAsync(_configuration);
        }

        protected override void Dispose(bool disposing)
        {
            _configuration?.Dispose();
            base.Dispose(disposing);
        }

        public virtual async Task InitializeAsync() => await SetConfigurationAsync();

        public Task DisposeAsync() => Task.CompletedTask;
    }
}