using IPB2.FigureStoreManagementSystem.MVCwithHttpClient.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<FigureApiClient>(client =>
{
    var baseAddress = builder.Configuration["Api:BaseUrl"] ?? "https://localhost:7001/";
    client.BaseAddress = new Uri(baseAddress);
});
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Figures}/{action=Index}/{id?}");

app.Run();
