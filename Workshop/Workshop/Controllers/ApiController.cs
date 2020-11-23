using System.Net.Http;
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
        public IActionResult Register([FromBody] PersonRequestDto person)
        {
            var user = personService.Register(new Person(person));
            if (user.Name != null && user.Password != null)
            {
                return Ok(new PersonResponseDto(user));
            }
            return BadRequest(new {error = "Please input all data"});
        }
    }
}