using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Workshop.Data;
using Workshop.Models;
using Workshop.Models.Dto.Requests;
using Workshop.Models.Dto.Responses;
using static System.String;

namespace Workshop.Services
{
    public class BookService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public BookService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<BookInfoResponse>> GetAllBooks()
        {
            var allBooks = await applicationDbContext.Books.Include(c => c.Category).Include(p => p.Person).ToListAsync();
            
             var books = allBooks!.Select(book => new BookInfoResponse
             {
                 Id = book.Id,
                 Name = book.Name,
                 IsAvailable = book.IsAvailable,
                 Category = new CategoryInfoResponse(book.Category),
                 BookOwner = allBooks.Any(b => b.Person != null) ? new UserInfoResponse(book.Person) : null
             });
            return books;
        }
        
         public async Task<Book> FindBookById(int bookId)
         {
             return await applicationDbContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);
         }
         
         public async Task UpdateBookOwner(Book book, Person person)
         {
             book.Person = person;
             book.IsAvailable = false;
             applicationDbContext.Update(book);
             await applicationDbContext.SaveChangesAsync();
         }
    }
}