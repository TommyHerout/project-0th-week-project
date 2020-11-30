using FoxInvaders.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoxInvaders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        private readonly IHelloService helloService;

        public HelloController(IHelloService helloService)
        {
            this.helloService = helloService;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return helloService.SayHello();
        }
    }
}
