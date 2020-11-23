namespace Workshop.Models.Dto
{
    public class RegisterResponseDto
    {
        public string Username { get; set; }
        public bool IsLibrarian { get; set; }
        
        public RegisterResponseDto(Person person)
        {
            Username = person.Username;
            IsLibrarian = person.IsLibrarian;
        }

        public RegisterResponseDto()
        {
        }
    }
}