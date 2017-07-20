namespace WebApi.Test.Application.Services
{
    internal class Service : IService
    {
        public BarResponse Get(string id)
        {
            return new BarResponse { Data = "Data for " + id };
        }
    }
}