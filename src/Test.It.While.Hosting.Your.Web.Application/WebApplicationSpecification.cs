using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test.It.While.Hosting.Your.Web.Application
{
    /// <summary>
    /// A BDD opinionated test specification for integration testing a webb application on a test host
    /// </summary>
    /// <typeparam name="TWebApplicationHost">The test host for the webb application</typeparam>
    public abstract class WebApplicationSpecification<TWebApplicationHost> 
        where TWebApplicationHost : IWebApplicationHost, new()
    {
        /// <summary>
        /// Runs the specification
        /// </summary>
        /// <param name="host">The webb applications test host</param>
        /// <param name="settings">Specification settings</param>
        public async Task SetConfigurationAsync(
            TWebApplicationHost host,
            Settings settings)
        {
            settings ??= new Settings();

            Server = await host.StartAsync(new SimpleTestConfigurer(Given), settings.ExecutionCancellationToken)
                .ConfigureAwait(false);

            try
            {
                await WhenAsync(settings.ExecutionCancellationToken)
                    .ConfigureAwait(false);
                // ReSharper disable once MethodSupportsCancellation
                // Support simple override option that does not enforce optional arguments to be defined
                await WhenAsync()
                    .ConfigureAwait(false);
            }
            finally
            {
                if (settings.StopAutomatically)
                {
                    await host.StopAsync(settings.StoppingCancellationToken)
                        .ConfigureAwait(false);
                }
            }
        }
        
        /// <summary>
        /// Runs the specification
        /// </summary>
        /// <param name="host">The webb applications test host</param>
        /// <param name="cancellationToken">Cancels the specification execution</param>
        /// <param name="stopCancellationToken">Cancels stopping the test host</param>
        [Obsolete("Use the SetConfigurationAsync overload")]
        public Task SetConfigurationAsync(
            TWebApplicationHost host, 
            CancellationToken cancellationToken = default,
            CancellationToken stopCancellationToken = default) =>
            SetConfigurationAsync(host, new Settings(cancellationToken, stopCancellationToken));

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