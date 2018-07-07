using System;
using System.Net;
using System.Net.Http;
using Should. Fluent;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using WebApi.Test.Application;
using Xunit;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    namespace Given_a_exception_throwing_application
    {
        public class When_getting_a_bar_from_foo :
            XUnitWindowsServiceSpecification<
                DefaultWebApplicationHostStarter<
                    WebApiTestWebApplicationBuilder<
                        ExceptionThrowingDuringCallApplication>>>
        {
            private readonly Exception _exceptionCaught;
            private HttpResponseMessage _result;

            public When_getting_a_bar_from_foo() : base(false)
            {
                try
                {
                    SetConfiguration();
                }
                catch (Exception exception)
                {
                    _exceptionCaught = exception;
                }
            }

            protected override void When()
            {
                _result = Client.GetAsync("foo/fooId/bar").Result;
            }

            [Fact]
            public void It_should_have_caught_an_exception()
            {
                _exceptionCaught.Should().Not.Be.Null();
            }

            [Fact]
            public void It_should_have_received_consuelas_message()
            {
                _exceptionCaught.Message.Should().Equal("Misser Superman no here.");
            }

            [Fact]
            public void It_should_have_gotten_internal_server_error_during_call()
            {
                _result.StatusCode.Should().Equal(HttpStatusCode.InternalServerError);
            }
        }
    }
}