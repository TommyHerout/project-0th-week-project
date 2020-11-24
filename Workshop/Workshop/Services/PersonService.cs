using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.Data;
using Workshop.Extensions;
using Workshop.Models;
using Workshop.Models.Dto;
using Workshop.Models.Dto.Requests;

namespace Workshop.Services
{
    public class PersonService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly BookService bookService;

        public PersonService(ApplicationDbContext applicationDbContext, BookService bookService)
        {
            this.applicationDbContext = applicationDbContext;
            this.bookService = bookService;
        }

        public async Task<Person> Register(Person person)
        {
            var register = (await applicationDbContext.Persons.AddAsync(person)).Entity;
            await applicationDbContext.SaveChangesAsync();
            return register;
        }

        public async Task<bool> DoesPersonExist(string username)
        {
            return await applicationDbContext.Persons.AnyAsync(p => p.Username == username);
        }

        public async Task<Person> FindPersonByUsername(string username)
        {
            return await applicationDbContext.Persons.FirstOrDefaultAsync(p => p.Username == username);
        }

        public async Task<int> Promote(PromoteRequest person)
        {
            var currentPerson = await FindPersonByUsername(person.Username);
            currentPerson.IsLibrarian ^= true;
            return await applicationDbContext.SaveChangesAsync();
        }
    }
}