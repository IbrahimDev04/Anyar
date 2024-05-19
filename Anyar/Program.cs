using Microsoft.EntityFrameworkCore;
using Anyar.DataAccessLayer;
using Anyar.Models;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AnyarContext>(option => option
    .UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;

})
    .AddEntityFrameworkStores<AnyarContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute("areas", "{area:exists}/{controller=slider}/{action=index}/{id?}");
app.MapControllerRoute("defaut", "{controller=home}/{action=index}/{id?}");

app.Run();

