using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;

namespace Test.It.While.Hosting.Your.Web.Application
{
    public abstract class WebApplicationSpecification<TWebApplicationHostStarter> : IDisposable
        where TWebApplicationHostStarter : IWebApplicationHostStarter, new()
    {
        private readonly ConcurrentBag<Exception> _exceptions = new ConcurrentBag<Exception>();

        public async Task SetConfigurationAsync(TWebApplicationHostStarter webHostingFixture)
        {
            webHostingFixture.OnUnhandledException += RegisterException;

            Client = webHostingFixture.Start(new SimpleTestConfigurer(Given));

            await WhenAsync();

            await ThrowOnExceptions();
        }

        private void RegisterException(Exception exception)
        {
            if (_exceptions.Contains(exception))
            {
                return;
            }

            if (!(exception is AggregateException aggregateException))
            {
                _exceptions.Add(exception);
                return;
            }

            foreach (var innerException in aggregateException.Flatten().InnerExceptions.Where(innerException => _exceptions.Contains(innerException) == false))
            {
                _exceptions.Add(innerException);
            }
        }

        private Task ThrowOnExceptions()
        {
            if (_exceptions.Any() == false)
            {
                return Task.CompletedTask;
            }

            if (_exceptions.Count == 1)
            {
                return Task.FromException(ExceptionDispatchInfo.Capture(_exceptions.First()).SourceException);
            }

            return Task.FromException(new AggregateException(_exceptions));
        }

        /// <summary>
        /// HttpClient to communicate with the hosted web application.
        /// </summary>
        protected HttpClient Client { get; private set; } = new HttpClient(new NotStartedHttpMessageHandler());

        /// <summary>
        /// OBS! <see cref="Client"/> is not ready here since the application is in bootstrapping phase where you control the service configuration.
        /// </summary>
        /// <param name="configurer">Service container</param>
        protected virtual void Given(IServiceContainer configurer) { }

        /// <summary>
        /// Application has started and can be called with <see cref="Client"/>.
        /// </summary>
        protected virtual Task WhenAsync()
        {
            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            Client?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}