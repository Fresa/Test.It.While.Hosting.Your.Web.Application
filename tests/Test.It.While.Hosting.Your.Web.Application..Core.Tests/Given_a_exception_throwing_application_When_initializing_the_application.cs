using System;
using System.Threading.Tasks;
using FluentAssertions;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using Test.It.While.Hosting.Your.Web.Application.Utils;
using WebApi.Core.Test.Application;
using Xunit;

namespace Test.It.While.Hosting.Your.Web.Application.Core.Tests
{
    public partial class Given_a_exception_throwing_application
    {
        public class When_initializing_the_application :
            XUnitWindowsServiceSpecification<
                DefaultWebApplicationHostStarter<ExceptionThrowingDuringSetupApplication>>
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