using System;
using Microsoft.Owin.Hosting;

namespace WebApi.Test.Application
{
    public class Program
    {
        private static void Main()
        {
            const string baseAddress = "http://localhost:9000/";

            using (WebApp.Start(url: baseAddress))
            {
                Console.WriteLine($"Listening on {baseAddress}");
                Console.WriteLine("Press ENTER to stop the web server.");
                Console.ReadLine();
            }
        }
    }
}