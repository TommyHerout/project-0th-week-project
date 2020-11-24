namespace Workshop.Models.Dto.Responses
{
    public class RegisterResponse
    {
        public string Username { get; set; }
        public bool IsLibrarian { get; set; }
        
        public RegisterResponse(Person person)
        {
            Username = person.Username;
            IsLibrarian = person.IsLibrarian;
        }

        public RegisterResponse()
        {
        }
    }
}