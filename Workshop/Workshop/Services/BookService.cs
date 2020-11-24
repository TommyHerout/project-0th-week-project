using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.Data;
using Workshop.Models;
using Workshop.Models.Dto.Responses;

namespace Workshop.Services
{
    public class BookService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BookService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<GetBooksResponse>> GetAllBooks()
        {
            var allBooks = await applicationDbContext.Books.Include(c => c.Category).Include(p => p.Person).ToListAsync();
            var books = allBooks.Select(book => new GetBooksResponse
            {
                Name = book.Name,
                IsAvailable = book.IsAvailable,
                Category = book.Category,
                Person = new UserInfoResponse(book.Person)
            });
            return books;
        }
        
        private async Task<Book> FindBookById(int bookId)
        {
            return await applicationDbContext.Books.FirstOrDefaultAsync(p => p.Id == bookId);
        }
        
        public async Task BorrowBook(Book book)
        {
            var read = await FindBookById(book.Id);
            read.IsAvailable = book.IsAvailable = false;
            read.Person.Id = book.Person.Id;
            await applicationDbContext.SaveChangesAsync();
        }
    }
}