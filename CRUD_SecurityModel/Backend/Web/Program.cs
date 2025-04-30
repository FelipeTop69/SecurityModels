using System.Text;
using System.Text.Json.Serialization;
using Business;
using Business.GeneralMapper;
using Business.Interfaces;
using Business.Services;
using Data;
using Data.Interfaces;
using Entity.Context;
using Entity.DTOs;
using Entity.DTOs.FormModuleDTOs;
using Entity.DTOs.RolFormPermissionDTOs;
using Entity.DTOs.RolUserDTOs;
using Entity.DTOs.UserDTOs;
using Entity.Model;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using Strategy.Context;
using Strategy.Implementations;
using Strategy.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

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
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero, // quita los 5 min de tolerancia

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
        )
    };
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<DbContextFactory>();
builder.Services.AddScoped(provider =>
{
    var factory = provider.GetRequiredService<DbContextFactory>();
    return factory.CreateDbContext();
});

builder.Services.AddScoped(typeof(LogicalDeleteStrategy<>));
builder.Services.AddScoped(typeof(PermanentDeleteStrategy<>));
builder.Services.AddScoped(typeof(IDeleteStrategyResolver<>), typeof(DeleteContext<>));

/// Definicion de Servicios 
builder.Services.AddScoped<IBusiness<FormDTO, FormDTO>, FormBusiness>();
builder.Services.AddScoped<IData<Form>, FormData>();

builder.Services.AddScoped<IBusiness<PersonDTO,PersonDTO>, PersonBusiness>();
builder.Services.AddScoped<IData<Person>, PersonData>();

builder.Services.AddScoped<IBusiness<ModuleDTO,ModuleDTO>,ModuleBusiness>();
builder.Services.AddScoped<IData<Module>,ModuleData>();

builder.Services.AddScoped<IBusiness<RolDTO, RolDTO>, RolBusiness>();
builder.Services.AddScoped<IData<Rol>, RolData>();

builder.Services.AddScoped<IBusiness<PermissionDTO,PermissionDTO>,PermissionBusiness>();
builder.Services.AddScoped<IData<Permission>,PermissionData>();

builder.Services.AddScoped<IBusiness<UserDTO, UserOptionsDTO>, UserBusiness>();
builder.Services.AddScoped<IData<User>, UserData>();
builder.Services.AddScoped<UserBusiness>();
builder.Services.AddScoped<UserData>();

builder.Services.AddScoped<IBusiness<FormModuleDTO,FormModuleOptionsDTO>, FormModuleBusiness>();
builder.Services.AddScoped<IData<FormModule>,FormModuleData>();

builder.Services.AddScoped<IBusiness<RolFormPermissionDTO,RolFormPermissionOptionsDTO>,RolFormPermissionBusiness>();
builder.Services.AddScoped<IData<RolFormPermission>,RolFormPermissionData>();

builder.Services.AddScoped<IBusiness<RolUserDTO, RolUserOptionsDTO>, RolUserBusiness>();
builder.Services.AddScoped<IData<RolUser>, RolUserData>();

builder.Services.AddScoped<TokenService>();

builder.Services.AddAutoMapper(typeof(GeneralMapper));

var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(',');

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(opcionesCORS =>
    {
        opcionesCORS.WithOrigins(origenesPermitidos)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Secrets.json", optional: true, reloadOnChange: true);

var app = builder.Build();

app.UseMiddleware<DbContextMiddleware>();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

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
