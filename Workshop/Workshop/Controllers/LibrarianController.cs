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

        public LibrarianController(PersonService personService)
        {
            this.personService = personService;
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
    }
}