namespace WebApi.Core.Test.Application.Services
{
    public class Service : IService
    {
        public BarResponse Get(string id)
        {
            return new BarResponse { Data = "Data for " + id };
        }
    }
}