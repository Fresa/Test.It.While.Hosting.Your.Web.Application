using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Test.It.While.Hosting.Your.Web.Application.Utils;
using WebApi.Core.Test.Application;
using WebApi.Core.Test.Application.Services;
using Xunit;

namespace Test.It.While.Hosting.Your.Web.Application.Core.Tests
{
    public partial class Given_a_foo_bar_application
    {
        public class When_getting_a_bar_from_foo :
            XUnitWindowsServiceSpecification<
                DefaultTestWebApplicationHost<Startup>>
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

            protected override async Task WhenAsync(CancellationToken cancellationToken)
            {
                _response = await Server.CreateClient().GetAsync("foo/fooId/bar", cancellationToken);
                _content = JsonConvert.DeserializeObject<BarResponse>(await _response.Content.ReadAsStringAsync());
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
            public void It_should_request_foo_with_id()
            {
                _idUsedToGet.Should().Be("fooId");
            }
        }
    }
}
