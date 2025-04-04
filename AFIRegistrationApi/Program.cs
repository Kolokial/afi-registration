using AFIRegistration.Requests;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RegistrationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("RegistrationsContextSQLite"))); builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "AFIRegistrationsAPI";
    config.Title = "AFI Registrations API v1";
    config.Version = "v1";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "RegistrationsAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapPost("/customer", async (RegistrationRequest customer, RegistrationDbContext db) =>
{
    db.Customer.Add();
    await db.SaveChangesAsync();

    return Results.Created($"/customer/{customer.Id}", customer);
});

app.Run();