using System;
using System.Collections.ObjectModel;
using System.Linq;
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
            XUnitWindowsServiceSpecification<
                DefaultWebApplicationHostStarter<
                    WebApiTestWebApplicationBuilder<
                        ExceptionThrowingDuringCallApplication>>>
        {
            private HttpResponseMessage _result;
            private ReadOnlyCollection<Exception> _exceptions;

            public override async Task InitializeAsync() => await base.InitializeAsync()
                .ContinueWith(task => _exceptions = task.Exception?.Flatten().InnerExceptions);

            protected override async Task WhenAsync()
            {
                _result = await Client.GetAsync("foo/fooId/bar");
            }

            [Fact]
            public void It_should_have_caught_an_exception()
            {
                _exceptions.Should().HaveCount(1);
            }

            [Fact]
            public void It_should_have_received_consuelas_message()
            {
                _exceptions.Single().Message.Should().Be("Misser Superman no here.");
            }

            [Fact]
            public void It_should_have_gotten_internal_server_error_during_call()
            {
                _result.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            }
        }
    }
}