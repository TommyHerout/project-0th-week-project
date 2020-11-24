using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.Models;
using Workshop.Models.Dto;
using Workshop.Services;

namespace Workshop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly PersonService personService;
        private readonly JwtAuthenticationManager jwtAuthenticationManager;

        public ApiController(PersonService personService, JwtAuthenticationManager jwtAuthenticationManager)
        {
            this.personService = personService;
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
        
        [AllowAnonymous]
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
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return StatusCode(406, new {error = "Please input all data"});
            }

            var token = await jwtAuthenticationManager.Authenticate(login.Username, login.Password);
            if (token is null)
            {
                return Unauthorized(new {error = "Incorrect username or password"});
            }
            return Ok(new {apiKey = token});
        }
    }
}