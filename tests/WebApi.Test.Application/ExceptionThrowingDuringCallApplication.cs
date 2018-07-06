using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using WebApi.Test.Application.Services;

namespace WebApi.Test.Application
{
    public class ExceptionThrowingDuringCallApplication : IApplication
    {
        public HttpConfiguration HttpConfiguration { get; } = new HttpConfiguration();

        public void Configure(IAppBuilder appBuilder, Action<Container> reconfigure)
        {
            appBuilder.UseWebApi(HttpConfiguration);
            ConfigureApplication(reconfigure);
        }

        private void ConfigureApplication(Action<Container> reconfigure)
        {
            var container = ConfigureContainer(reconfigure);
            HttpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            HttpConfiguration.Formatters.Clear();
            HttpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());

            HttpConfiguration.MapHttpAttributeRoutes();
        }

        private static Container ConfigureContainer(Action<Container> reconfigure)
        {
            var container = new Container();

            container.RegisterSingleton<IService, SupermanService>();
            container.RegisterSingleton<IMaidService, ConsuelasService>();

            reconfigure(container);
            container.Verify();

            return container;
        }
    }
}