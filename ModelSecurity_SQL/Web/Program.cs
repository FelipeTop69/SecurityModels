using Business;
using Data;
using Entity.Context;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<ApplicationDbContext>(
//    //options=>options.UseSqlServer("name=SQLServer")
//    //options=>options.UseNpgsql("name=PostgreSQL")
//);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Mi API", Version = "v1" });

    // Agrega el header personalizado X-DB-Provider
    c.AddSecurityDefinition("X-DB-Provider", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Name = "X-DB-Provider",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Description = "Provea el proveedor de base de datos (sqlserver, postgresql, mysql)"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "X-DB-Provider"
                }
            },
            new string[] { }
        }
    });
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DbContextFactory>();
builder.Services.AddScoped(provider =>
{
    var factory = provider.GetRequiredService<DbContextFactory>();
    return factory.CreateDbContext();
});


/// Definicion de Servicios 
builder.Services.AddScoped<FormBusiness>();
builder.Services.AddScoped<FormData>();

builder.Services.AddScoped<PersonBusiness>();
builder.Services.AddScoped<PersonData>();

builder.Services.AddScoped<ModuleBusiness>();
builder.Services.AddScoped<ModuleData>();

builder.Services.AddScoped<RolBusiness>();
builder.Services.AddScoped<RolData>();

builder.Services.AddScoped<PermissionBusiness>();
builder.Services.AddScoped<PermissionData>();

builder.Services.AddScoped<UserBusiness>();
builder.Services.AddScoped<UserData>();

builder.Services.AddScoped<FormModuleBusiness>();
builder.Services.AddScoped<FormModuleData>();

builder.Services.AddScoped<RolFormPermissionBusiness>();
builder.Services.AddScoped<RolFormPermissionData>();

builder.Services.AddScoped<RolUserBusiness>();
builder.Services.AddScoped<RolUserData>();

var app = builder.Build();

app.UseMiddleware<DbContextMiddleware>();

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
