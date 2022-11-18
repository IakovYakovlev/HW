using HW_20.Controllers;
using HW_20.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<AccountController>();
builder.Services.AddTransient<IPhoneBookRepository, PhoneBookRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddHttpClient<PhoneBookRepository>();
builder.Services.AddHttpClient<UserService>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
