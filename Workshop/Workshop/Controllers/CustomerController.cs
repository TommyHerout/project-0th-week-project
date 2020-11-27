using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Extensions;
using Workshop.Models;
using Workshop.Models.Dto.Requests;
using Workshop.Models.Dto.Responses;
using Workshop.Services;
using Workshop.Services.Interfaces;

namespace Workshop.Controllers
{
    [Authorize]
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly IBookService bookService;
        private readonly IJwtAuthenticationService jwtAuthenticationService;
        private readonly IMapper mapper;

        public CustomerController(IPersonService personService, IBookService bookService,
                             IJwtAuthenticationService jwtAuthenticationService,
                             IMapper mapper)
        {
            this.personService = personService;
            this.bookService = bookService;
            this.jwtAuthenticationService = jwtAuthenticationService;
            this.mapper = mapper;
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

            var registerMapper = mapper.Map<RegisterResponse>(user);
            return Ok(registerMapper);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto login)
        {
            if (string.IsNullOrEmpty(login.Username) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest(new ErrorResponse(ErrorTypes.DataMissing.EnumDescription()));
            }

            var user = await jwtAuthenticationService.Authenticate(login.Username, login.Password);
            if (user is null)
            {
                return Unauthorized(new ErrorResponse(ErrorTypes.IncorrectCredentials.EnumDescription()));
            }
            return Ok(new {apiKey = user});
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
            
            var borrowMapper = mapper.Map<BorrowResponse>(borrow);
            return Ok(borrowMapper);
        }
    }
}