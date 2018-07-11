using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using WebApi.Test.Application;
using Xunit;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    public partial class Given_a_exception_throwing_application
    {
        public class When_getting_a_bar_from_foo :
            XUnitWindowsServiceSpecificationAsync<
                DefaultWebApplicationHostStarter<
                    WebApiTestWebApplicationBuilder<
                        ExceptionThrowingDuringCallApplication>>>
        {
            private HttpResponseMessage _result;
            private Exception _exception;

            public override async Task InitializeAsync() => await base.InitializeAsync()
                .ContinueWith(task => _exception = task.Exception?.InnerException, TaskContinuationOptions.OnlyOnFaulted);

            protected override async Task WhenAsync()
            {
                _result = await Client.GetAsync("foo/fooId/bar");
            }

            [Fact]
            public void It_should_have_caught_an_exception()
            {
                _exception.Should().NotBeNull();
            }

            [Fact]
            public void It_should_have_received_consuelas_message()
            {
                _exception.Message.Should().Be("Misser Superman no here.");
            }

            [Fact]
            public void It_should_have_gotten_internal_server_error_during_call()
            {
                _result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            }
        }
    }
}