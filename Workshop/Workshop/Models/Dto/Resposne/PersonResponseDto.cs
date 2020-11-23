namespace Workshop.Models.Dto
{
    public class PersonResponseDto
    {
        public string Username { get; set; }
        public bool IsLibrarian { get; set; }
        
        public PersonResponseDto(Person person)
        {
            Username = person.Name;
            IsLibrarian = person.IsLibrarian;
        }

        public PersonResponseDto()
        {
        }
    }
}