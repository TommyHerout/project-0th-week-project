using System.Collections.Generic;

namespace Workshop.Models.Dto.Responses
{
    public class BookInfoResponse
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public CategoryInfoResponse Category { get; set; }
        public UserInfoResponse BookOwner { get; set; }
    }
}