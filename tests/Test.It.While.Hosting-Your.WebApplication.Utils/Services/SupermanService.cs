using System;

namespace Test.It.While.Hosting.Your.Web.Application.Utils.Services
{
    public class SupermanService :  IService
    {
        private readonly IMaidService _maidService;

        public SupermanService(IMaidService maidService)
        {
            _maidService = maidService;
        }

        public BarResponse Get(string id)
        {
            throw new InvalidOperationException("Misser Superman no here.");
        }
    }
}