using System;
using System.Net.Http;

namespace Test.It.While.Hosting.Your.Web.Application
{
    internal class NoopServer : IServer
    {
        public Uri BaseAddress => 
            throw new InvalidOperationException("The server has not yet started.");

        public HttpMessageHandler CreateHttpMessageHandler()
        {
            throw new InvalidOperationException("The server has not yet started.");
        }

        public IWebSocketClient CreateWebSocketClient()
        {
            throw new InvalidOperationException("The server has not yet started.");
        }
    }
}