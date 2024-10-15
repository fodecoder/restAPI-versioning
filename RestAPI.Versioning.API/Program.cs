using Asp.Versioning;
using Common.DB.Extension;
using Microsoft.Extensions.Options;
using RestAPI.Versioning.API.Config;
using RestAPI.Versioning.Interfaces.Repository;
using RestAPI.Versioning.Interfaces.Service;
using RestAPI.Versioning.Services.AutoMapper;
using RestAPI.Versioning.Services.Context;
using RestAPI.Versioning.Services.Repository;
using RestAPI.Versioning.Services.Services;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder ( args );

// Add services to the container.

builder.Services.AddControllers ();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions> , ConfigureSwaggerOptions> ();
builder.Services.AddSwaggerGen ( options =>
    {
        // Add a custom operation filter which sets default values
        options.OperationFilter<SwaggerDefaultValues> ();
    } );
builder.Services
    .AddApiVersioning ( options =>
    {
        //indicating whether a default version is assumed when a client does
        // does not provide an API version.
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.DefaultApiVersion = new ApiVersion ( 1.0 );
    } )
    .AddApiExplorer ( options =>
    {
        // Add the versioned API explorer, which also adds IApiVersionDescriptionProvider service
        // note: the specified format code will format the version as "'v'major[.minor][-status]"
        options.GroupNameFormat = "'v'VVV";

        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
        // can also be used to control the format of the API version in route templates
        options.SubstituteApiVersionInUrl = true;
    } );

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
    app.UseSwaggerUI ( options =>
    {
        var descriptions = app.DescribeApiVersions ();

        // Build a swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            options.SwaggerEndpoint ( $"/swagger/{description.GroupName}/swagger.json" , description.GroupName.ToUpperInvariant () );
        }
    } );
}

app.UseHttpsRedirection ();

app.UseAuthorization ();

app.MapControllers ();

app.UseRouting ();

app.Run ();
