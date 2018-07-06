using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Test.It.While.Hosting.Your.Web.Application
{
    internal class NotStartedHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new InvalidOperationException("The server has not yet started.");
        }
    }
}