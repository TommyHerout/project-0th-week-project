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

        public PersonService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
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
    }
}