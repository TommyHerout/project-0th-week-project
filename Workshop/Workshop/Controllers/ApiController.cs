using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workshop.Models;
using Workshop.Models.Dto;
using Workshop.Models.Dto.Requests;
using Workshop.Models.Dto.Responses;
using Workshop.Services;

namespace Workshop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        private readonly PersonService personService;
        private readonly BookService bookService;
        private readonly JwtAuthenticationService jwtAuthenticationService;

        public ApiController(PersonService personService, BookService bookService,
                             JwtAuthenticationService jwtAuthenticationService)
        {
            this.personService = personService;
            this.bookService = bookService;
            this.jwtAuthenticationService = jwtAuthenticationService;
            
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
            return Ok(new RegisterResponse(user));
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return StatusCode(406, new {error = "Please input all data"});
            }
            var token = await jwtAuthenticationService.Authenticate(login.Username, login.Password);
            if (token is null)
            {
                return Unauthorized(new {error = "Incorrect username or password"});
            }
            return Ok(new {apiKey = token});
        }

        [HttpGet("get-book-list")]
        public async Task<ActionResult> GetAllBooks()
        {
            var allBooks = await bookService.GetAllBooks();
            if (allBooks is null)
            {
                return StatusCode(418, new {error = "There are no books in a library.. what?"});
            }
            return Ok(allBooks);
        }

        [HttpPost("promote")]
        public async Task<ActionResult> PromoteToLibrarian([FromBody] PromoteRequest person)
        {
            var exists = await personService.DoesPersonExist(person.Username);
            if (exists != null)
            {
                return StatusCode(418, new {error = "No user with this username was found."}); 
            }
            await personService.Promote(person);
            return Ok(new PromoteResponse());
        }
    }
}