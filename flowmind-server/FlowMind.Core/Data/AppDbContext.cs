using FlowMind.Core.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlowMind.Core.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");

            // 1. אכיפת אינדקס ייחודי על שדה האימייל ברמת הדאטאבייס (אבטחה מקסימלית)
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // 2. הגדרת שדה האימייל כחובה והגבלת אורך למניעת מתקפות DoS
            builder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            // 3. הגבלת אורך לשמות המשתמש (מניעת ניצול לרעה של נפח זיכרון)
            builder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            // // 4. הגבלת אורך לשדה התפקיד (הרול)
            // builder.Entity<User>()
            //     .Property(u => u.Role)
            //     .IsRequired()
            //     .HasMaxLength(20);

            // 5. הגבלת אורך לשדה המטבע (למשל "ILS", "USD" - מספיקים 5 תווים לביטחון)
            builder.Entity<User>()
                .Property(u => u.PreferredCurrency)
                .IsRequired()
                .HasMaxLength(5);
        }

        // 6. דריסת פונקציית השמירה לעדכון תאריכי יצירה אוטומטיים במערכת
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added && entry.Entity.GetType().GetProperty("CreatedAt") != null)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        
       }
}