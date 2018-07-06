using System;
using System.Web.Http;
using Owin;
using SimpleInjector;

namespace WebApi.Test.Application
{
    public interface IApplication
    {
        void Configure(IAppBuilder appBuilder, Action<Container> reconfigure);
        HttpConfiguration HttpConfiguration { get; }
    }
}