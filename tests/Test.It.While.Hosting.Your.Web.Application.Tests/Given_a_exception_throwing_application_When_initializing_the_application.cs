using System;
using System.Threading.Tasks;
using FluentAssertions;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using WebApi.Test.Application;
using Xunit;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    public partial class Given_a_exception_throwing_application2
    {
        public class When_initializing_the_application :
            XUnitWindowsServiceSpecificationAsync<
                DefaultWebApplicationHostStarter<
                    WebApiTestWebApplicationBuilder<
                        ExceptionThrowingDuringSetupApplication>>>
        {
            private Exception _exceptionCaught;

            public override async Task InitializeAsync() => await base.InitializeAsync()
                .ContinueWith(task => _exceptionCaught = task.Exception?.InnerException,
                    TaskContinuationOptions.OnlyOnFaulted);

            [Fact]
            public void It_should_catch_an_exception_during_setup()
            {
                _exceptionCaught.Should().NotBeNull();
            }
        }
    }
}