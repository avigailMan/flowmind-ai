using FlowMind.Core.Data;
using Microsoft.EntityFrameworkCore;
using FlowMind.API.Extensions;
using FlowMind.Core.Repositories;
using FlowMind.Core.Interfaces;
using FlowMind.API.Services;
using FlowMind.Server.Middleware.Extensions;
using System;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();

// 2. הזרקת ה-DbContext למערכת וחיבורו ל-PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, b => b.MigrationsAssembly("FlowMind.API")));
// הגדרנו שקבצי הארכיטקטורה של הדאטאבייס (Migrations) יישמרו בתוך פרויקט ה-API

builder.Services.AddIdentityServices(builder.Configuration);//JWT Authentication

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
        {
            policy.AllowAnyHeader()
                  .AllowAnyMethod()
                  .WithOrigins("http://localhost:4200"); 
        });
});

var app = builder.Build();

app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();