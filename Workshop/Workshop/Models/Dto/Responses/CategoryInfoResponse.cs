#nullable enable
namespace Workshop.Models.Dto.Responses
{
    public class CategoryInfoResponse
    {
        public string? Name { get; set; }

        public CategoryInfoResponse(Category? category)
        {
            Name = category?.Name;
        }
    }
}