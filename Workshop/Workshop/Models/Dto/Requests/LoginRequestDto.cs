namespace Workshop.Models.Dto.Requests
{
    public class LoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        
        public LoginRequestDto(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public LoginRequestDto()
        {
        }
    }
}