using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.Data;
using Workshop.Extensions;
using Workshop.Models;
using Workshop.Models.Dto;

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

        public async Task<Person> DoesPersonExist(string username)
        {
            return await applicationDbContext.Persons.FirstOrDefaultAsync(p => p.Username == username);
        }

        public async Task<Person> FindPersonById(int personId)
        {
            return await applicationDbContext.Persons.FirstOrDefaultAsync(p => p.Id == personId);
        }
    }
}