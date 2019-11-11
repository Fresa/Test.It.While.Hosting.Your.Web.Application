using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Test.It.While.Hosting.Your.Web.Application
{
    public interface IWebSocketClient
    {
        Task<WebSocket> ConnectAsync(Uri uri, CancellationToken cancellationToken = default);
    }
}