using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Test.It.While.Hosting.Your.Web.Application.HostStarters
{
    internal class WebApiServiceContainer : IServiceContainer
    {
        private readonly IServiceCollection _serviceCollection;

        private readonly Lazy<IServiceProvider> _serviceProviderResolver;
        private IServiceProvider ServiceProvider => _serviceProviderResolver.Value;

        public WebApiServiceContainer(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
            _serviceProviderResolver = new Lazy<IServiceProvider>(() => _serviceCollection.BuildServiceProvider());
        }

        public void Dispose()
        {
        }

        public void Register<TImplementation>(Func<TImplementation> configurer) where TImplementation : class
        {
            _serviceCollection.Replace(ServiceDescriptor.Transient(provider => configurer()));
        }

        public void Register<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _serviceCollection.Replace(ServiceDescriptor.Transient<TService, TImplementation>());
        }

        public void RegisterSingleton<TImplementation>(Func<TImplementation> configurer) where TImplementation : class
        {
            _serviceCollection.Replace(ServiceDescriptor.Singleton(provider => configurer()));
        }

        public void RegisterSingleton<TService, TImplementation>()
            where TService : class where TImplementation : class, TService
        {
            _serviceCollection.Replace(ServiceDescriptor.Singleton<TService, TImplementation>());
        }

        public TService Resolve<TService>() where TService : class
        {
            return ServiceProvider.GetService<TService>();
        }
    }
}