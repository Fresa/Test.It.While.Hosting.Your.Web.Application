using System.Net.Http;

namespace Test.It.While.Hosting.Your.Web.Application.Core.Tests
{
    internal static class ServerExtensions
    {
        internal static HttpClient CreateClient(this IServer server)
        {
            return new HttpClient(server.CreateHttpMessageHandler())
            {
                BaseAddress = server.BaseAddress
            };
        }
    }
}