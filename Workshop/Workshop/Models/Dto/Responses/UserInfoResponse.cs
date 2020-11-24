namespace Workshop.Models.Dto.Responses
{
    public class UserInfoResponse
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public int BorrowedBooks { get; set; }
        
        public UserInfoResponse(Person person)
        {
            Name = person.Name;
            Username = person.Username;
            BorrowedBooks = person.Books.Count;
        }

        public UserInfoResponse()
        {
        }
    }
}