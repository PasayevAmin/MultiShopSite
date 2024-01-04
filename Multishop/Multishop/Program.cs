using Microsoft.EntityFrameworkCore;
using Multishop.DAL;
using Multishop.Middlewares;
using Multishop.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(
    opt =>opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);
builder.Services.AddScoped<_LayoutService>();


var app = builder.Build();

app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllerRoute(
    "area",
    "{area:exists}/{controller=Dashboard}/{action=index}/{id?}"
    );

app.UseStaticFiles();

//app.UseSession();
//app.UseMiddleware<GlobalExceptionMiddlewares>();

app.MapControllerRoute(
    "area",
    "{controller=Home}/{action=index}/{id?}"
    );
app.Run();