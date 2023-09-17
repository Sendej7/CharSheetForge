using AutoMapper;
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.DTO;
using webapi.Interfaces;
using webapi.Models.DND;
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

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.CreateMap<DndCharacterDto, DndCharacter>();
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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
