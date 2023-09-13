using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Interfaces;
using webapi.Repositories;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CharSheetContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("CharSheetContext")));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICharacterSheetRepository, CharacterSheetRepository>();
builder.Services.AddScoped<ICharacterSheetService, CharacterSheetService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
