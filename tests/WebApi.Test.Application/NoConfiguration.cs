using Test.It;
using Test.It.Specifications;

namespace WebApi.Test.Application
{
    internal class NoConfiguration : ITestConfigurer
    {
        public void Configure(IServiceContainer serviceContainer)
        {
            
        }
    }
}