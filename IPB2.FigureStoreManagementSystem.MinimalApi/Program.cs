var app = WebApplication.CreateBuilder(args).Build();

app.MapGet("/", () => "Figure Store Minimal API is running.");

app.Run();
