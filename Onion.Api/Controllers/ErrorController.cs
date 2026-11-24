using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Api.Errors;

namespace Onion.Api.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ErrorController : ControllerBase
    {
        [HttpGet]
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponse(code));
        }
    }
}
