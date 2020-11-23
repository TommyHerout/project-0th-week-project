using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Workshop.Data;
using Workshop.Extensions;

namespace Workshop.Services
{
    public class JwtAuthenticationManager
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly string apiKey = "My super awesome key";

        public JwtAuthenticationManager(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            if (!await applicationDbContext.Persons.AnyAsync(
                p => p.Username == username && p.Password == password.HashString()))
            {
                return null;
            };
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(apiKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}