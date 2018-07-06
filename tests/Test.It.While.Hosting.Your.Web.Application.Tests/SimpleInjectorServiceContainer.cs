using System;
using SimpleInjector;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    internal class SimpleInjectorServiceContainer : IServiceContainer
    {
        private readonly Container _container;

        public SimpleInjectorServiceContainer(Container container)
        {
            _container = container;
            _container.Options.AllowOverridingRegistrations = true;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public void Register<TImplementation>(Func<TImplementation> configurer) where TImplementation : class
        {
            _container.Register(configurer);
        }

        public void Register<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>();
        }

        public void RegisterSingleton<TImplementation>(Func<TImplementation> configurer) where TImplementation : class
        {
            _container.RegisterSingleton(configurer);
        }

        public void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _container.RegisterSingleton<TService, TImplementation>();
        }

        public TService Resolve<TService>() where TService : class
        {
            _container.Options.AllowOverridingRegistrations = false;
            return _container.GetInstance<TService>();
        }
    }
}