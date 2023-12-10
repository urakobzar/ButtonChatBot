using Microsoft.AspNetCore.Authentication.Cookies;
using SoftPortalBot.Model.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using SoftPortalBot.Model.StartingData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>
                (options =>
                    options.UseSqlServer(
                        "Server = (localdb)\\mssqllocaldb; Database = mobilesdb; Trusted_Connection = True;"));


//StartingData.AddAllData();

// Установка конфигурации подключения.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => //CookieAuthenticationOptions
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();  
app.UseAuthorization();  
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
