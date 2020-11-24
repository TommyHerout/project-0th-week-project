using System.Collections.Generic;

namespace Workshop.Models.Dto.Responses
{
    public class GetBooksResponse
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public Category Category { get; set; }
        public UserInfoResponse Person { get; set; }
    }
}