// using Microsoft.AspNetCore.Identity;

namespace FlowMind.Core.Models
{
    public class ApplicationUser
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; } = string.Empty;
// 3. הסיסמה המוצפנת (ה-Hash) - לעולם לא שומרים סיסמה רגילה!
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        
        // 4. ה"מלח" (Salt) הקריפטוגרפי - מחרוזת רנדומלית ייחודית לכל משתמש שמגינה מפני מתקפות מילון
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // המטבע המועדף לתצוגה במערכת (למשל: "ILS", "USD")
        public string PreferredCurrency { get; set; } = "ILS";


    }
}