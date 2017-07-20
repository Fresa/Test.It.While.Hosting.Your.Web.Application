using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using WebApi.Test.Application.Services;

namespace WebApi.Test.Application.Controllers
{
    public class FooController : ApiController
    {
        private readonly IService _service;

        public FooController(IService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("foo/{id}/bar", Name = "Bar")]
        [ResponseType(typeof(BarResponse))]
        public IHttpActionResult Bar([FromUri] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _service.Get(id);

            return new NegotiatedContentResult<BarResponse>(HttpStatusCode.OK, result, this);
        }
    }
}