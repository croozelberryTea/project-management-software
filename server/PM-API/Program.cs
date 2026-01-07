using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PM_API;
using PM_API.Common.Constants;
using PM_API.Configuration;
using PM_API.Core.Interfaces.Repositories;
using PM_API.Core.Interfaces.Services;
using PM_API.Infrastructure;
using PM_API.Service.Services;
using Scalar.AspNetCore;

using PM_API.Infrastructure.Model;
using PM_API.Middleware;
using PM_API.Repositories.Interfaces;
using PM_API.Service.Repositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = ConnectionStringHelper.GetConnectionString();

Console.WriteLine("Using database connection string from environment variables.");

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
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<ITicketService, TicketService>();
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

app.UseMiddleware<ExceptionMiddleware>();
// app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthorization();
app.Run();