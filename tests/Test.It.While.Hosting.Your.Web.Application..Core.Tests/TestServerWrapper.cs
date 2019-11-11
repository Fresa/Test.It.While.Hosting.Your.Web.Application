using System;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;

namespace Test.It.While.Hosting.Your.Web.Application.Utils
{
    internal sealed class TestServerWrapper : IServer
    {
        private readonly TestServer _testServer;

        public TestServerWrapper(TestServer testServer)
        {
            _testServer = testServer;
        }

        public Uri BaseAddress => _testServer.BaseAddress;

        public HttpMessageHandler CreateHttpMessageHandler()
        {
            return _testServer.CreateHandler();
        }

        public IWebSocketClient CreateWebSocketClient()
        {
            return new TestServerWebSocketClient(_testServer.CreateWebSocketClient());
        }
    }
}