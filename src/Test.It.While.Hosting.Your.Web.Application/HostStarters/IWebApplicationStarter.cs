#if NET462
using Owin;

namespace Test.It.While.Hosting.Your.Web.Application.HostStarters
{
    public interface IWebApplicationStarter
    {
        void Start(IAppBuilder appBuilder);
    }
}
#endif

#if NETCOREAPP2_1
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Test.It.While.Hosting.Your.Web.Application.HostStarters
{
    public interface IWebApplicationStarter
    {
        void Start(IApplicationBuilder applicationBuilder);
    }

    //public class Testar
    //{
    //    public Testar()
    //    {
    //        var a = new TestServer(new WebHostBuilder().UseStartup<>());
    //        var client = a.CreateClient();
    //    }
    //}
}
#endif