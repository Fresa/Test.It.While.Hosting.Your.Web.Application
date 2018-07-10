namespace Test.It.While.Hosting.Your.Web.Application.Utils.Services
{
    public class Service : IService
    {
        public BarResponse Get(string id)
        {
            return new BarResponse { Data = "Data for " + id };
        }
    }
}