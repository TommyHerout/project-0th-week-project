using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.Models;
using Workshop.Models.Dto.Requests;
using Workshop.Models.Dto.Responses;

namespace Workshop.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<UserInfoResponse>> GetAllCustomersInfo();
        Task<Person> Register(Person person);
        Task<Person> FindPersonByUsername(string username);
        Person DoesPersonExists(string username);
        string GetPersonJwtUsername();
        Task Promote(PromoteRequest person);
        Task BorrowBook(BorrowInfo borrow);
    }
}