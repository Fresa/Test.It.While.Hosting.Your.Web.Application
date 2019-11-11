using System;
using System.Net.Http;

namespace Test.It.While.Hosting.Your.Web.Application
{
    public interface IServer
    {
        Uri BaseAddress { get; }
        HttpMessageHandler CreateHttpMessageHandler();
        IWebSocketClient CreateWebSocketClient();
    }
}