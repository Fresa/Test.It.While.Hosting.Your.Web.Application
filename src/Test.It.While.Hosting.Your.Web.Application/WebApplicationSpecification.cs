using System.Threading;
using System.Threading.Tasks;

namespace Test.It.While.Hosting.Your.Web.Application
{
    public abstract class WebApplicationSpecification<TWebApplicationHost> 
        where TWebApplicationHost : IWebApplicationHost, new()
    {
        public async Task SetConfigurationAsync(
            TWebApplicationHost host, 
            CancellationToken cancellationToken = default,
            CancellationToken stopCancellationToken = default)
        {
            Server = await host.StartAsync(new SimpleTestConfigurer(Given), cancellationToken)
                .ConfigureAwait(false);

            try
            {
                await WhenAsync(cancellationToken)
                    .ConfigureAwait(false);
                // ReSharper disable once MethodSupportsCancellation
                // Support simple override option that does not enforce optional arguments to be defined
                await WhenAsync()
                    .ConfigureAwait(false);
            }
            finally
            {
                await host.StopAsync(stopCancellationToken)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>
        /// HttpClient to communicate with the hosted web application.
        /// </summary>
        protected IServer Server { get; private set; } = new NoopServer();

        /// <summary>
        /// OBS! <see cref="Server"/> is not ready here since the application is in bootstrapping phase where you control the service configuration.
        /// </summary>
        /// <param name="configurer">Service container</param>
        protected virtual void Given(IServiceContainer configurer) { }

        /// <summary>
        /// Application has started and can be called with <see cref="Server"/>.
        /// </summary>
        protected virtual Task WhenAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Application has started and can be called with <see cref="Server"/>.
        /// </summary>
        protected virtual Task WhenAsync()
        {
            return Task.CompletedTask;
        }
    }
}