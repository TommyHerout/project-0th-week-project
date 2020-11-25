using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Workshop.Extensions;
using Workshop.Models;
using Workshop.Models.Dto;
using Workshop.Models.Dto.Requests;
using Workshop.Models.Dto.Responses;
using Workshop.Services;

namespace Workshop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("customer")]
    public class ApiController : ControllerBase
    {
        private readonly PersonService personService;
        private readonly BookService bookService;
        private readonly JwtAuthenticationService jwtAuthenticationService;
        private readonly CategoryService categoryService;

        public ApiController(PersonService personService, BookService bookService,
                             JwtAuthenticationService jwtAuthenticationService,
                             CategoryService categoryService)
        {
            this.personService = personService;
            this.bookService = bookService;
            this.jwtAuthenticationService = jwtAuthenticationService;
            this.categoryService = categoryService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDto register)
        {
            if (string.IsNullOrEmpty(register.Name) || string.IsNullOrEmpty(register.Username) ||
                string.IsNullOrEmpty(register.Password))
            {
                return BadRequest(new ErrorResponse(ErrorTypes.DataMissing.EnumDescription()));
            }
            var doesExists = await personService.FindPersonByUsername(register.Username);
            if (doesExists != null)
            {
                return BadRequest(new ErrorResponse(ErrorTypes.UsernameExists.EnumDescription()));
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
                return BadRequest(new ErrorResponse(ErrorTypes.DataMissing.EnumDescription()));
            }

            var token = await jwtAuthenticationService.Authenticate(login.Username, login.Password);
            if (token is null)
            {
                return Unauthorized(new ErrorResponse(ErrorTypes.IncorrectCredentials.EnumDescription()));
            }
            return Ok(new {apiKey = token});
        }
        
        [HttpGet("get-book-list")]
        public async Task<ActionResult> GetAllBooks()
        {
            var allBooks = await bookService.GetAllBooks();
            if (allBooks is null)
            {
                return BadRequest(new ErrorResponse(ErrorTypes.Empty.EnumDescription()));
            }
            return Ok(allBooks);
        }

        [HttpPost("borrow")]
        public async Task<ActionResult> BorrowBook([FromBody] BorrowRequest borrowRequest)
        {
            var currentPerson = personService.GetPersonJwtUsername();
            
            var book = await bookService.FindBookById(borrowRequest.BookId);
            var person = await personService.FindPersonByUsername(currentPerson);

            if (book is null || !book.IsAvailable)
            {
                return BadRequest(new ErrorResponse(ErrorTypes.NotFound.EnumDescription()));
            }

            var borrow = new BorrowInfo(person, book);
            await personService.BorrowBook(borrow);
            await bookService.UpdateBookOwner(book, person);
            return Ok(new BorrowResponse(borrow));
        }
    }
}