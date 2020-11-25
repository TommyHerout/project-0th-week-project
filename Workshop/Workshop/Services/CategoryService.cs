using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.Data;
using Workshop.Models;
using Workshop.Models.Dto.Requests;

namespace Workshop.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext applicationDbContext;
        
        public CategoryService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        
        public Category FindCategoryById(int categoryId)
        {
            return applicationDbContext.Categories.Include(b => b.Books).FirstOrDefault(c => c.Id == categoryId);
        }
        
        public async Task<(string book, string category)> AssignToCategory(int bookId, int categoryId)
        {
            var book = await applicationDbContext.Books.Include(c => c.Category).FirstOrDefaultAsync(b => b.Id == bookId);
            var category = await applicationDbContext.Categories.FindAsync(categoryId);

            if (book is null || category is null)
            {
                return (null, null);
            }

            book.Category = category;
            applicationDbContext.Update(book);
            await applicationDbContext.SaveChangesAsync();
            return (book.Name, category.Name);
        }
    }
}