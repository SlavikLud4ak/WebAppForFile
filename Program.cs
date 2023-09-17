using WebAppForFile.Options;
using WebAppForFile.Services.Abstract;
using WebAppForFile.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var services = builder.Services;

services.Configure<AzureOption>(builder.Configuration.GetSection("Azure"));
services.AddTransient<IFileServices, FileServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
