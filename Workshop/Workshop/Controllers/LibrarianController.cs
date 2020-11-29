using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Extensions;
using Workshop.Models.Dto.Requests;
using Workshop.Models.Dto.Responses;
using Workshop.Services;
using Workshop.Services.Interfaces;

namespace Workshop.Controllers
{
    [Authorize(Roles = "True")] // True because IsLibrarian is bool
    [ApiController]
    [Route("librarian")]
    public class LibrarianController : ControllerBase
    {
        private readonly IPersonService personService;
        private readonly ICategoryService categoryService;

        public LibrarianController(IPersonService personService, ICategoryService categoryService)
        {
            this.personService = personService;
            this.categoryService = categoryService;
        }
        
        [HttpPost("promote")]
        public async Task<ActionResult> PromoteToLibrarian([FromBody] PromoteRequest person)
        {
            var exists = personService.DoesPersonExists(person.Username);

            if (exists is null)
            {
                return BadRequest(new ErrorResponse(ErrorTypes.Empty.EnumDescription()));
            }
            await personService.Promote(person);
            return Ok(new PromoteResponse());
        }

        [HttpPost("assign")]
        public async Task<ActionResult> AssignToCategory([FromBody] AssignCategoryRequest request)
        {
            if (request is null)
            {
                return BadRequest(new ErrorResponse(ErrorTypes.Empty.EnumDescription()));
            }
            var response = await categoryService.AssignToCategory(request.BookId, request.CategoryId);
            return Ok(new AssignCategoryResponse(response));
        }

        [HttpGet("all-customers-info")]
        public async Task<ActionResult> GetAllCustomers()
        {
            var getAllBooks = await personService.GetAllCustomersInfo();
            return Ok(getAllBooks);
        }
    }
}