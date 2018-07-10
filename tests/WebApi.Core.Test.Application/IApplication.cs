using System;
using Microsoft.AspNetCore.Builder;
using SimpleInjector;

namespace WebApi.Core.Test.Application
{
    public interface IApplication
    {
        void Configure(IApplicationBuilder appBuilder, Action<Container> reconfigure);
    }
}