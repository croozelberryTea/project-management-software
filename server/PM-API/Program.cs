using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PM_API;
using PM_API.Configuration;
using PM_API.Infrastructure;
using Scalar.AspNetCore;

using PM_API.Infrastructure.Model;

var builder = WebApplication.CreateBuilder(args);

var connectionString = GetConnectionString();

Console.WriteLine($"Using connection string: {connectionString}");

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Smtp"));

// register EF Core DbContext with Npgsql provider
builder.Services.AddDbContext<ProjectManagementDbContext>(options =>
    options.UseNpgsql(connectionString));

// register ASP.NET Identity (stores use the identity DbContext)
builder.Services.AddIdentity<User, IdentityRole<long>>()
    .AddEntityFrameworkStores<ProjectManagementDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient(typeof(IEmailSender<>), typeof(SmtpEmailSender<>));

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapIdentityApi<User>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // adds UI at /scalar/v1
}

// app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthorization();
app.Run();

#region Local Functions

string GetConnectionString()
{
    var host = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
    var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
    var database = Environment.GetEnvironmentVariable("DB_DATABASE_NAME") ?? "pm_db";
    var username = Environment.GetEnvironmentVariable("DB_USERNAME") ?? "postgres";
    var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";
    
    return $"Host={host};Port={port};Database={database};Username={username};Password={password};Command Timeout=360;";
}


#endregion