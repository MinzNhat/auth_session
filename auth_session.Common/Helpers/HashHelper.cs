using System.Text;
using System.Security.Cryptography;

namespace auth_session.Common.Helpers
{
    public static class HashHelper
    {
        public static string HashPassword(string password)
        {
            return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
        }
    }
}