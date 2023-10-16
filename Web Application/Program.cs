using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Models;
using Repository;
using Web_Application;

public class Program
{
    //public static async Task handel(HttpContext context)
    //{
    //    if (context.Request.Path == "/")
    //    {
    //        await context.Response.WriteAsync("<h1>Welcome to ASP.NET World</h1>");
    //    }
    //    else if (context.Request.Path == "/product")
    //    {
    //        MyDBContext myDB = new MyDBContext();
    //        await context.Response.WriteAsJsonAsync(myDB.Products.ToList());
    //    }
    //    else
    //    {
    //        await context.Response.WriteAsync("NOT found");
    //    }
    //}
    public static int Main(string[] args)
    {
        WebApplicationBuilder builder =
             WebApplication.CreateBuilder();

        #region DI Container
        builder.Services.AddDbContext<MyDBContext>(i =>
        {
            i.UseLazyLoadingProxies().UseSqlServer(
                builder.Configuration.GetConnectionString("MyDB"));
        });

        builder.Services.AddIdentity<User, IdentityRole>(i => i.User.RequireUniqueEmail = true)
            .AddEntityFrameworkStores<MyDBContext>();

        builder.Services.AddScoped(typeof(UnitOfWork));
        builder.Services.AddScoped(typeof(ProductManager));
        builder.Services.AddScoped(typeof(CategoryManeger));
        builder.Services.AddScoped(typeof(AccountManger));
        builder.Services.AddControllersWithViews(); 
        #endregion

        var webApp = builder.Build();

        #region Middel Were
        webApp.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory() + "/Content"),
            RequestPath = ""

        });
        webApp.UseAuthentication();
        webApp.UseAuthorization();
        webApp.MapControllerRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");

# endregion

        webApp.Run();


        return 0;
    }
}