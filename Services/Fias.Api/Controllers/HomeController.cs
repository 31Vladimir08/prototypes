using Microsoft.AspNetCore.Mvc;

namespace Fias.Api.Controllers
{
    [ApiController]
    [Route("api/ping")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Index()
        {
            return Ok("pong");
        }
    }
}
