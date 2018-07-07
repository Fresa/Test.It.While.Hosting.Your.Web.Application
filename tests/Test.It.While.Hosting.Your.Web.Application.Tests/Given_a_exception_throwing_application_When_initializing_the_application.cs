using System;
using Should. Fluent;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using WebApi.Test.Application;
using Xunit;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    namespace Given_a_exception_throwing_application
    {
        public class When_initializing_the_application :
            XUnitWindowsServiceSpecification<
                DefaultWebApplicationHostStarter<
                    WebApiTestWebApplicationBuilder<
                        ExceptionThrowingDuringSetupApplication>>>
        {
            private readonly Exception _exceptionCaught;

            public When_initializing_the_application() : base(false)
            {
                try
                {
                    SetConfiguration();
                }
                catch (Exception ex)
                {
                    _exceptionCaught = ex;
                }
            }

            [Fact]
            public void It_should_catch_an_exception_during_setup()
            {
                _exceptionCaught.Should().Not.Be.Null();
            }
        }
    }
}