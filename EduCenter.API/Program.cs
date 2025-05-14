using EduCenter.API.Data;
using EduCenter.API.Shared.Services.Hashing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Add Swagger generation

builder.Services.AddDbContext<DatabaseContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DevConnection")));

builder.Services.Configure<Argon2Options>(options => { });
builder.Services.AddSingleton<IPasswordHasher, Argon2PasswordHasher>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
