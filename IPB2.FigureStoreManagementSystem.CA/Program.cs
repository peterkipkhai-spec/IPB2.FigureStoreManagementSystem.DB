using IPB2.FigureStoreManagementSystem.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var connectionString = configuration.GetConnectionString("FigureStore")
    ?? Environment.GetEnvironmentVariable("FIGURE_STORE_CONNECTION_STRING")
    ?? throw new InvalidOperationException("Missing DB connection string. Configure FigureStore or FIGURE_STORE_CONNECTION_STRING.");

services.AddDbContext<FigureStoreDbContext>(options => options.UseSqlServer(connectionString));
var serviceProvider = services.BuildServiceProvider();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== Figure Store Management (Console App) ===");
    Console.WriteLine("1. List all figures");
    Console.WriteLine("2. Create new figure");
    Console.WriteLine("3. Update figure stock");
    Console.WriteLine("4. Delete figure");
    Console.WriteLine("5. List categories");
    Console.WriteLine("0. Exit");
    Console.Write("Choose menu: ");

    var key = Console.ReadLine();

    switch (key)
    {
        case "1":
            await ListFigures(serviceProvider);
            break;
        case "2":
            await CreateFigure(serviceProvider);
            break;
        case "3":
            await UpdateStock(serviceProvider);
            break;
        case "4":
            await DeleteFigure(serviceProvider);
            break;
        case "5":
            await ListCategories(serviceProvider);
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Invalid menu.");
            Pause();
            break;
    }
}

static async Task ListFigures(IServiceProvider serviceProvider)
{
    await using var scope = serviceProvider.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<FigureStoreDbContext>();

    var figures = await db.FsFigures
        .Include(x => x.Category)
        .OrderBy(x => x.FigureId)
        .ToListAsync();

    Console.WriteLine("\n--- Figure List ---");
    foreach (var item in figures)
    {
        Console.WriteLine($"[{item.FigureId}] {item.FigureName} | Category: {item.Category?.CategoryName ?? "N/A"} | Price: {item.Price} | Stock: {item.Stock}");
    }

    Pause();
}

static async Task CreateFigure(IServiceProvider serviceProvider)
{
    await using var scope = serviceProvider.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<FigureStoreDbContext>();

    Console.Write("Figure name: ");
    var name = Console.ReadLine();

    Console.Write("Description: ");
    var description = Console.ReadLine();

    Console.Write("Price: ");
    decimal.TryParse(Console.ReadLine(), out var price);

    Console.Write("Stock: ");
    int.TryParse(Console.ReadLine(), out var stock);

    Console.Write("Category ID (optional): ");
    int.TryParse(Console.ReadLine(), out var categoryId);

    var entity = new FsFigure
    {
        FigureName = string.IsNullOrWhiteSpace(name) ? "Unnamed Figure" : name.Trim(),
        Description = description,
        Price = price,
        Stock = stock,
        CategoryId = categoryId == 0 ? null : categoryId
    };

    db.FsFigures.Add(entity);
    await db.SaveChangesAsync();

    Console.WriteLine($"Created figure with ID: {entity.FigureId}");
    Pause();
}

static async Task UpdateStock(IServiceProvider serviceProvider)
{
    await using var scope = serviceProvider.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<FigureStoreDbContext>();

    Console.Write("Figure ID: ");
    if (!int.TryParse(Console.ReadLine(), out var figureId))
    {
        Console.WriteLine("Invalid ID.");
        Pause();
        return;
    }

    var figure = await db.FsFigures.FirstOrDefaultAsync(x => x.FigureId == figureId);
    if (figure is null)
    {
        Console.WriteLine("Figure not found.");
        Pause();
        return;
    }

    Console.Write($"Current stock ({figure.Stock}): ");
    if (!int.TryParse(Console.ReadLine(), out var newStock))
    {
        Console.WriteLine("Invalid stock.");
        Pause();
        return;
    }

    figure.Stock = newStock;
    await db.SaveChangesAsync();
    Console.WriteLine("Stock updated.");
    Pause();
}

static async Task DeleteFigure(IServiceProvider serviceProvider)
{
    await using var scope = serviceProvider.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<FigureStoreDbContext>();

    Console.Write("Figure ID to delete: ");
    if (!int.TryParse(Console.ReadLine(), out var figureId))
    {
        Console.WriteLine("Invalid ID.");
        Pause();
        return;
    }

    var figure = await db.FsFigures.FirstOrDefaultAsync(x => x.FigureId == figureId);
    if (figure is null)
    {
        Console.WriteLine("Figure not found.");
        Pause();
        return;
    }

    db.FsFigures.Remove(figure);
    await db.SaveChangesAsync();
    Console.WriteLine("Figure deleted.");
    Pause();
}

static async Task ListCategories(IServiceProvider serviceProvider)
{
    await using var scope = serviceProvider.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<FigureStoreDbContext>();

    var categories = await db.FsCategories.OrderBy(x => x.CategoryId).ToListAsync();

    Console.WriteLine("\n--- Category List ---");
    foreach (var item in categories)
    {
        Console.WriteLine($"[{item.CategoryId}] {item.CategoryName}");
    }

    Pause();
}

static void Pause()
{
    Console.WriteLine("\nPress Enter to continue...");
    Console.ReadLine();
}
