using Microsoft.EntityFrameworkCore;
using PostManAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<ProfileContext>(options =>
    options.UseSqlite
    (builder.Configuration.GetConnectionString("PostContext") ?? throw new InvalidOperationException("Connection string 'PostContext' not found.")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();