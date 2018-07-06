using System;

namespace WebApi.Test.Application.Services
{
    internal class SupermanService :  IService
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