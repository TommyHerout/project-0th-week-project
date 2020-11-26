using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.Models;
using Workshop.Models.Dto.Requests;

namespace Workshop.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAllCustomers();
        Task<Person> Register(Person person);
        Task<Person> FindPersonByUsername(string username);
        Person DoesPersonExists(string username);
        string GetPersonJwtUsername();
        Task Promote(PromoteRequest person);
        Task BorrowBook(BorrowInfo borrow);
    }
}