using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using Test.It.While.Hosting.Your.Web.Application.Utils;
using Test.It.While.Hosting.Your.Web.Application.Utils.Services;
using WebApi.Test.Application;
using Xunit;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    public partial class Given_a_foo_bar_application2
    {
        public class When_getting_a_bar_from_foo :
            XUnitWindowsServiceSpecification<
                DefaultWebApplicationHostStarter<
                    WebApiTestWebApplicationBuilder<
                        FooBarApplication>>>
        {
            private string _idUsedToGet;
            private BarResponse _content;
            private HttpResponseMessage _response;

            protected override void Given(IServiceContainer configurer)
            {
                var service = new TestService("testData");
                service.OnGet += (sender, id) => _idUsedToGet = id;

                configurer.RegisterSingleton<IService>(() => service);
            }

            protected override async Task WhenAsync()
            {
                _response = await Client.GetAsync("foo/fooId/bar");
                _content = await _response.Content.ReadAsAsync<BarResponse>();
            }

            [Fact]
            public void It_should_respond_with_200()
            {
                _response.StatusCode.Should().Be(HttpStatusCode.OK);
            }

            [Fact]
            public void It_should_respond_with_bar_content_data()
            {
                _content.Data.Should().Be("testData");
            }

            [Fact]
            public void It_should_rrequest_foo_with_id()
            {
                _idUsedToGet.Should().Be("fooId");
            }
        }
    }
}