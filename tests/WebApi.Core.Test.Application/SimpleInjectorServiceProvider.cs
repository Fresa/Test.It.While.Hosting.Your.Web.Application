using System;
using SimpleInjector;

namespace WebApi.Core.Test.Application
{
    internal class SimpleInjectorServiceProvider : IServiceProvider
    {
        private readonly Container _container;
        private readonly IServiceProvider _currentServiceProvider;

        public SimpleInjectorServiceProvider(Container container, IServiceProvider currentServiceProvider)
        {
            _container = container;
            _currentServiceProvider = currentServiceProvider;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.GetInstance(serviceType);
            }
            catch
            {
                return _currentServiceProvider.GetService(serviceType);
            }
        }
    }
}