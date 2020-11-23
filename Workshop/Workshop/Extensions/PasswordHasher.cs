using System;
using System.Security.Cryptography;
using System.Text;

namespace Workshop.Extensions
{
    public static class PasswordHasher
    {
        public static string HashString(this String str)
        {
            using SHA256 hashing = SHA256.Create();
            byte[] data = hashing.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }             
            var hash = sBuilder.ToString();
            return hash;
        }
    }
}