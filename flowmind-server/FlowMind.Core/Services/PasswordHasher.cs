using System.Security.Cryptography;
using System.Text;

namespace FlowMind.Core.Services
{
    public static class PasswordHasher
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key; // יצירת "מלח" רנדומלי ייחודי לכל משתמש
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // חישוב ה-Hash של הסיסמה עם ה"מלח"
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // חישוב ה-Hash של הסיסמה שהמשתמש הזין עם ה"מלח" השמור
                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != storedHash[i]) return false;
                }
                return true;

            }
        }
    }

}