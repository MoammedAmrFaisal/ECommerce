using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Api.Errors;
using Onion.Repository.Data;

namespace Onion.Api.Controllers
{
   public class BuggyController : APIBaseController
    {
        private readonly OnionContext _dbcontext;
        public BuggyController(OnionContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFoundResult()
        {
            var Product = _dbcontext.Products.Find(100);
            if (Product is null) return NotFound(new ApiResponse(404));
            return Ok(Product);
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            var Product = _dbcontext.Products.Find(100);
            var ProductToReturn = Product.ToString();
            return Ok(ProductToReturn);
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ProblemDetails { Title = "Bad Request" });
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return BadRequest(new ProblemDetails { Title = $"Bad Request with id: {id}" });
        }
    }
}
