using System;
using System.Configuration;
using System.Linq;
using Microsoft.Owin.Hosting;

namespace WebApi.Test.Application
{
    internal static class StartOptionsExtensions
    {
        public static StartOptions UseAppStartupFromAppConfig(this StartOptions startOptions)
        {
            const string key = "owin:appStartup";
            var values = ConfigurationManager.AppSettings.GetValues(key);
            if (values == null || values.Any() == false)
            {
                throw new InvalidOperationException($"Could not find any startup appSettings in app.config with key {key}.");
            }

            startOptions.AppStartup = values.Last();

            return startOptions;
        }
    }
}