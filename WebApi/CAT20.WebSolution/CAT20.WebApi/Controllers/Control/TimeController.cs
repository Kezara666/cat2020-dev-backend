using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CAT20.WebApi.Controllers.Control
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetServerTime()
        {
            DateTime serverTime = DateTime.UtcNow;
            return Ok(serverTime);
        }
    }
}
