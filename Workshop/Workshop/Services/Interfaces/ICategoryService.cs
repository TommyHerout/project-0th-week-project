using System.Threading.Tasks;
using Workshop.Models;

namespace Workshop.Services.Interfaces
{
    public interface ICategoryService
    {
        Category FindCategoryById(int categoryId);
        Task<(string book, string category)> AssignToCategory(int bookId, int categoryId);
    }
}