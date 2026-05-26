using FlowMind.Core.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. הזרקת ה-DbContext למערכת וחיבורו ל-PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("FlowMind.API")));
    // הגדרנו שקבצי הארכיטקטורה של הדאטאבייס (Migrations) יישמרו בתוך פרויקט ה-API

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();