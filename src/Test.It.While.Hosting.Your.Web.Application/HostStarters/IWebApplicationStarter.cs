using Owin;

namespace Test.It.While.Hosting.Your.Web.Application.HostStarters
{
    public interface IWebApplicationStarter
    {
        void Start(IAppBuilder appBuilder);
    }
}