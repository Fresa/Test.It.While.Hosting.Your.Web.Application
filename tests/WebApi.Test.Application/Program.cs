using System;
using Microsoft.Owin.Hosting;

namespace WebApi.Test.Application
{
    public class Program
    {
        private static void Main()
        {
            const string baseAddress = "http://localhost:9000/";
            var startOptions = new StartOptions(baseAddress).UseAppStartupFromAppConfig();

            using (WebApp.Start(startOptions))
            {
                Console.WriteLine($"{Type.GetType(startOptions.AppStartup)} has started, listening on {baseAddress}");
                Console.WriteLine("Press ENTER to stop the web server.");
                Console.ReadLine();
            }
        }
    }
}