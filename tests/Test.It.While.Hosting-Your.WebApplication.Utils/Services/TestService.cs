using System;

namespace Test.It.While.Hosting.Your.Web.Application.Utils.Services
{
    public class TestService : IService
    {
        private readonly string _testData;

        public TestService(string testData)
        {
            _testData = testData;
        }

        public event EventHandler<string> OnGet;

        public BarResponse Get(string id)
        {
            OnGet?.Invoke(this, id);
            return new BarResponse { Data = _testData };
        }
    }
}