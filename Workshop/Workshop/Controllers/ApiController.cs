using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.Models;
using Workshop.Models.Dto;
using Workshop.Services;

namespace Workshop.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly PersonService personService;

        public ApiController(PersonService personService)
        {
            this.personService = personService;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDto register)
        {
            if (string.IsNullOrEmpty(register.Name) || string.IsNullOrEmpty(register.Username) || string.IsNullOrEmpty(register.Password))
            {
                return BadRequest(new {error = "Please input all data"});
            }

            var doesExists = await personService.DoesPersonExist(register.Username);
            if (doesExists != null)
            {
                return BadRequest(new {error = "This username already exists.."}); 
            }
            var user = await personService.Register(new Person(register));
            return Ok(new RegisterResponseDto(user));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return StatusCode(406, new {error = "Please input all data"});
            }
            
            if (await personService.Login(login.Username, login.Password))
            {
                return Ok(new {message = "You are successfully logged in."});
            }
            return BadRequest(new {error = "Username or password is incorrect."});
        }
    }
}