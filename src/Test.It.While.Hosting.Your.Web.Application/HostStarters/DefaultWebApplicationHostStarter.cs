#if NET47
using System.Net.Http;
using Microsoft.Owin.Testing;
using Owin;
using Test.It.Specifications;
using Test.It.While.Hosting.Your.Web.Application.ApplicationBuilders;

namespace Test.It.While.Hosting.Your.Web.Application.HostStarters
{
    public class DefaultWebApplicationHostStarter<TApplicationBuilder> : IWebApplicationHostStarter
        where TApplicationBuilder : IWebApplicationBuilder, new()
    {
        private TestServer _testServer;

        public HttpClient Start(ITestConfigurer testConfigurer)
        {
            void Startup(IAppBuilder appBuilder)
            {
                appBuilder.Properties[OwinProperties.ExceptionHandler] = OnUnhandledException;

                var applicationBuilder = new TApplicationBuilder();
                applicationBuilder.CreateWith(testConfigurer).Start(appBuilder);
            }

            _testServer = TestServer.Create(Startup);

            return _testServer.HttpClient;
        }

        public event ExceptionHandler OnUnhandledException;

        public void Dispose()
        {
            _testServer?.Dispose();
        }
    }
}
#endif

#if NETCOREAPP2_1
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Test.It.Specifications;
using Test.It.While.Hosting.Your.Web.Application.ApplicationBuilders;

namespace Test.It.While.Hosting.Your.Web.Application.HostStarters
{
    public class DefaultWebApplicationHostStarter2<TApplicationBuilder> : IWebApplicationHostStarter
        where TApplicationBuilder : IWebApplicationBuilder, new()
    {
        private TestServer _testServer;

        public HttpClient Start(ITestConfigurer testConfigurer)
        {
            void Startup(IApplicationBuilder appBuilder)
            {
                appBuilder.Properties[OwinProperties.ExceptionHandler] = OnUnhandledException;

                var applicationBuilder = new TApplicationBuilder();
                applicationBuilder.CreateWith(testConfigurer).Start(appBuilder);
            }

            var webHostBuilder = new WebHostBuilder().Configure(Startup);
            _testServer = new TestServer(webHostBuilder);

            return _testServer.CreateClient();
        }

        public event ExceptionHandler OnUnhandledException;

        public void Dispose()
        {
            _testServer?.Dispose();
        }
    }

    public class DefaultWebApplicationHostStarter<TApplication> : IWebApplicationHostStarter
        where TApplication : class, IStartup
    {
        private TestServer _testServer;

        public HttpClient Start(ITestConfigurer testConfigurer)
        {
            void Startup(IApplicationBuilder appBuilder)
            {
                appBuilder.Properties[OwinProperties.ExceptionHandler] = OnUnhandledException;
            }

            var webHostBuilder = new WebHostBuilder()
                .Configure(Startup)
                .UseStartup<TApplication>()
                .ConfigureServices(collection => { testConfigurer.Configure(new CoreServiceContainer(collection)); });
            _testServer = new TestServer(webHostBuilder);

            return _testServer.CreateClient();
        }

        public event ExceptionHandler OnUnhandledException;

        public void Dispose()
        {
            _testServer?.Dispose();
        }
    }

    internal class CoreServiceContainer : IServiceContainer
    {
        private readonly IServiceCollection _serviceCollection;

        private readonly Lazy<IServiceProvider> _serviceProviderResolver;
        private IServiceProvider ServiceProvider => _serviceProviderResolver.Value;

        public CoreServiceContainer(IServiceCollection serviceCollection)
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

        public void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _serviceCollection.Replace(ServiceDescriptor.Singleton<TService, TImplementation>());
        }

        public TService Resolve<TService>() where TService : class
        {
            return ServiceProvider.GetService<TService>();
        }
    }
}
#endif