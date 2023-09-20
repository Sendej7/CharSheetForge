using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using webapi.Data;
using webapi.DTO;
using webapi.Interfaces;
using webapi.Models;
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

builder.Services.AddScoped<IGenericRepository<Equipment>, GenericRepository<Equipment>>(serviceProvider =>
        new GenericRepository<Equipment>(serviceProvider.GetRequiredService<CharSheetContext>()));

builder.Services.AddScoped<IGenericRepository<AllyAndOrganization>, GenericRepository<AllyAndOrganization>>(serviceProvider =>
        new GenericRepository<AllyAndOrganization>(serviceProvider.GetRequiredService<CharSheetContext>()));

builder.Services.AddScoped<IGenericRepository<AttackAndSpellcasting>, GenericRepository<AttackAndSpellcasting>>(serviceProvider =>
    new GenericRepository<AttackAndSpellcasting>(serviceProvider.GetRequiredService<CharSheetContext>()));

builder.Services.AddScoped<IGenericRepository<FeatureAndTrait>, GenericRepository<FeatureAndTrait>>(serviceProvider =>
    new GenericRepository<FeatureAndTrait>(serviceProvider.GetRequiredService<CharSheetContext>()));


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.MapType<SystemType>(() => new OpenApiSchema
    {
        Type = "string",
        Enum = Enum.GetValues(typeof(SystemType))
                    .Cast<SystemType>()
                    .Select(e => new OpenApiString(e.ToString()) as IOpenApiAny)
                    .ToList()
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));



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
