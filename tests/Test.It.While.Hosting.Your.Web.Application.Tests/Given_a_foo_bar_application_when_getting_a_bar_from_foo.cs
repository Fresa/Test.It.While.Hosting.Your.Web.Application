using System.Net;
using System.Net.Http;
using Should.Fluent;
using Test.It.While.Hosting.Your.Web.Application.HostStarters;
using WebApi.Test.Application;
using WebApi.Test.Application.Services;
using Xunit;

namespace Test.It.While.Hosting.Your.Web.Application.Tests
{
    namespace Given_a_foo_bar_application
    {
        public class When_getting_a_bar_from_foo2 :
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

            protected override void When()
            {
                _response = Client.GetAsync("foo/fooId/bar").Result;
                _content = _response.Content.ReadAsAsync<BarResponse>().Result;
            }

            [Fact]
            public void It_should_respond_with_200()
            {
                _response.StatusCode.Should().Equal(HttpStatusCode.OK);
            }

            [Fact]
            public void It_should_respond_with_bar_content_data()
            {
                _content.Data.Should().Equal("testData");
            }

            [Fact]
            public void It_should_rrequest_foo_with_id()
            {
                _idUsedToGet.Should().Equal("fooId");
            }
        }
    }
}