using Business;
using Data;
using Entity.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(
    options=>options.UseSqlServer("name=defaultConn")
);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
