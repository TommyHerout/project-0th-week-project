using System.Linq;
using System.Security.Claims;

namespace Workshop.Extensions
{
    public static class GetUserId
    {
        public static long GetUserID(this ClaimsPrincipal user)
        {
            return long.Parse(user.Claims.First(i => i.Type == "PersonId").Value);
        }
    }
}