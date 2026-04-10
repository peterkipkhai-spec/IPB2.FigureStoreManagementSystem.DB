using IPB2.FigureStoreManagementSystem.DB.Models;
using IPB2.FigureStoreManagementSystem.Domain.Contracts;
using IPB2.FigureStoreManagementSystem.WebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IFigureCatalogService, FigureCatalogService>();

var connectionString = builder.Configuration.GetConnectionString("FigureStore")
                      ?? Environment.GetEnvironmentVariable("FIGURE_STORE_CONNECTION_STRING");

if (!string.IsNullOrWhiteSpace(connectionString))
{
    builder.Services.AddDbContext<FigureStoreDbContext>(options => options.UseSqlServer(connectionString));
}
else
{
    builder.Services.AddDbContext<FigureStoreDbContext>();
}

var app = builder.Build();

app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

app.Run();
