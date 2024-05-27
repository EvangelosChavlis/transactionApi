using server.src.Application;
using server.src.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using server.WebApi.Middlewares;
using server.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "transaction API V1", Version = "v1" });
    c.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication(); // Add this line to register your application services

var app = builder.Build();

// Configure CORS policy
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    // Enable Swagger
    app.UseSwagger();

    // Configure Swagger UI for each endpoint
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "transaction API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseExceptionHandlingMiddleware();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
