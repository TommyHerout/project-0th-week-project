using Workshop.Data;
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

        public Person Register(Person person)
        {
            var register = applicationDbContext.Persons.Add(person).Entity;
            applicationDbContext.SaveChanges();
            return register;
        }
    }
}