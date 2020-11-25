using Workshop.Models.Dto.Requests;

namespace Workshop.Models.Dto.Responses
{
    public class AssignCategoryResponse
    {
        public string Book { get; set; }
        public string Category { get; set; }
        
        public AssignCategoryResponse((string book, string category) response)
        {
            Book = response.book;
            Category = response.category;
        }
    }
}