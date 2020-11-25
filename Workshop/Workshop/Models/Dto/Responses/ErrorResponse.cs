using System.ComponentModel;

namespace Workshop.Models.Dto.Responses
{
    public class ErrorResponse
    {
        public string Error { get; set; }
        
        public ErrorResponse(string error)
        {
            Error = error;
        }
    }
    public enum ErrorTypes
    {
        [Description("Please input all data.")]
        DataMissing,
        [Description("Username already exists.")]
        UsernameExists,
        [Description("Incorrect username or password")]
        IncorrectCredentials,
        [Description("The list is empty.")]
        Empty,
        [Description("No data was found.")]
        NotFound
    }
}