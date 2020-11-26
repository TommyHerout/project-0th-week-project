using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.Models;
using Workshop.Models.Dto.Responses;

namespace Workshop.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookInfoResponse>> GetAllBooks();
        Task<Book> FindBookById(int bookId);
        Task UpdateBookOwner(Book book, Person person);
    }
}