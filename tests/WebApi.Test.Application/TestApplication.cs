using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Test.It.Specifications;
using WebApi.Test.Application.Services;

namespace WebApi.Test.Application
{
    public class TestApplication
    {
        private readonly ITestConfigurer _reconfigurer;

        public TestApplication()
        {
            _reconfigurer = new NoConfiguration();
        }

        public TestApplication(ITestConfigurer reconfigurer)
        {
            _reconfigurer = reconfigurer;
        }

        public void Configuration(IAppBuilder appBuilder)
        {
            var config = HttpConfiguration();
            appBuilder.UseWebApi(config);
        }

        private HttpConfiguration HttpConfiguration()
        {
            var config = new HttpConfiguration { DependencyResolver = ConfigureContainer() };

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.MapHttpAttributeRoutes();

            return config;
        }

        private IDependencyResolver ConfigureContainer()
        {
            var container = new Container();

            container.RegisterSingleton<IService, Service>();

            _reconfigurer.Configure(new SimpleInjectorServiceContainer(container));
            return new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}