using HW_20.Data;
using HW_20.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDatabase(builder.Configuration.GetSection("Data"));
builder.Services.AddTransient<IPhoneBookRepository, PhoneBookRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
