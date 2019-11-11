using System.Threading.Tasks;
using Xunit;

namespace Test.It.While.Hosting.Your.Web.Application.Utils
{
    public abstract class XUnitWindowsServiceSpecification<TWebApplicationStarter> : WebApplicationSpecification<TWebApplicationStarter>, IAsyncLifetime
        where TWebApplicationStarter : class, IWebApplicationHost, new()
    {
        private TWebApplicationStarter _configuration;

        protected async Task SetConfigurationAsync()
        {
            _configuration = new TWebApplicationStarter();
            await SetConfigurationAsync(_configuration);
        }

        public virtual async Task InitializeAsync() => 
            await SetConfigurationAsync();

        public Task DisposeAsync()
        {
            _configuration?.Dispose();
            return Task.CompletedTask;
        }
    }
}