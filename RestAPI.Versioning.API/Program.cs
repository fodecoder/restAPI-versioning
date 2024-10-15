using Common.DB.Extension;
using RestAPI.Versioning.Interfaces.Repository;
using RestAPI.Versioning.Interfaces.Service;
using RestAPI.Versioning.Services.AutoMapper;
using RestAPI.Versioning.Services.Context;
using RestAPI.Versioning.Services.Repository;
using RestAPI.Versioning.Services.Services;

var builder = WebApplication.CreateBuilder ( args );

// Add services to the container.

builder.Services.AddControllers ();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddSwaggerGen ();

// Add Repositories
builder.Services.AddScoped<IItemRepository , ItemRepository> ();

// Add Services
builder.Services.AddScoped<IItemService , ItemService> ();

// Add AutoMapper
builder.Services.AddAutoMapper ( typeof ( InventoryProfile ) );

// Add Db Context
builder.Services.AddDatabase<InventoryDBContext> ();

var app = builder.Build ();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment ())
{
    app.UseSwagger ();
    app.UseSwaggerUI ();
}

app.UseHttpsRedirection ();

app.UseAuthorization ();

app.MapControllers ();

app.UseRouting ();

app.Run ();
