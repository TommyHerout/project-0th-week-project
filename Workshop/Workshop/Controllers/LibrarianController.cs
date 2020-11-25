using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workshop.Models.Dto.Requests;
using Workshop.Models.Dto.Responses;
using Workshop.Services;

namespace Workshop.Controllers
{
    [Authorize(Roles = "True")] // True because IsLibrarian is bool
    [ApiController]
    [Route("librarian")]
    public class LibrarianController : ControllerBase
    {
        private readonly PersonService personService;
        private readonly CategoryService categoryService;

        public LibrarianController(PersonService personService, CategoryService categoryService)
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
                return StatusCode(404, new {error = "Who?"});
            }
            await personService.Promote(person);
            return Ok(new PromoteResponse());
        }

        [HttpPost("assign")]
        public async Task<ActionResult> AssignToCategory([FromBody] AssignCategoryRequest request)
        {
            if (request is null)
            {
                return StatusCode(404);
            }
            var response = await categoryService.AssignToCategory(request.BookId, request.CategoryId);
            return Ok(new AssignCategoryResponse(response));
        }
    }
}