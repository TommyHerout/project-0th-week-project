using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workshop.Data;
using Workshop.Models;

namespace Workshop.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext applicationDbContext;
        
        public CategoryService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        
        public async Task<Category> FindCategoryById(int categoryId)
        {
            return await applicationDbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }
    }
}