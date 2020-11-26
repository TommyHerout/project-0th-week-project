using System.Threading.Tasks;

namespace Workshop.Services.Interfaces
{
    public interface IJwtAuthenticationService
    {
        Task<string> Authenticate(string username, string password);
    }
}