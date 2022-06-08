using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    /// <summary>
    /// /c Specifies that the platoforms controller resides in the "CommandsService".
    /// 
    /// The reason being, once we implement API Gateway, we need to be
    /// very clear between the routing for this Platforms controller and the
    /// PlatformsController defined in our "PlatformsService" microservice. 
    /// </summary>
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {

        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Inbound test from Platforms Controller successful.");
        }
    }
}