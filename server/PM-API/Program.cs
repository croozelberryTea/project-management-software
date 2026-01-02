using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PM_API;
using PM_API.Configuration;
using PM_API.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PMDb")
                       ?? Environment.GetEnvironmentVariable("PM_DB_CONNECTION")
                       ?? "Host=localhost;Database=pm_db;Username=postgres;Password=postgres";

Console.WriteLine($"Using connection string: {connectionString}");

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Smtp"));

// register EF Core DbContext with Npgsql provider
builder.Services.AddDbContext<ProjectManagementDbContext>(options =>
    options.UseNpgsql(connectionString));

// Set up authentication
builder.Services.AddDbContext<ProjectManagementIdentityDbContext>(
    options => options.UseNpgsql(connectionString));  // todo would it be better to use a diff connection string? diff db?

// register ASP.NET Identity (stores use the identity DbContext)
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ProjectManagementIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient(typeof(IEmailSender<>), typeof(SmtpEmailSender<>));

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Console.WriteLine("Running in development mode");
    app.MapOpenApi();
    app.MapScalarApiReference(); // adds UI at /scalar/v1
}
else
{
    Console.WriteLine("Running in production mode");
}

// app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthorization();
app.Run();