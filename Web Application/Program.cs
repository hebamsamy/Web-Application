using Microsoft.Extensions.FileProviders;
using Models;
using Repository;
using Web_Application;

public class Program
{
    public static async Task handel(HttpContext context)
    {
        if (context.Request.Path == "/")
        {
            await context.Response.WriteAsync("<h1>Welcome to ASP.NET World</h1>");
        }
        else if (context.Request.Path == "/product")
        {
            MyDBContext myDB = new MyDBContext();
            await context.Response.WriteAsJsonAsync(myDB.Products.ToList());
        }
        else
        {
            await context.Response.WriteAsync("NOT found");
        }
    }
    public static int Main(string[] args)
    {
        WebApplicationBuilder builder =
             WebApplication.CreateBuilder();
        //builder.Services.AddScoped<MyDBContext>();
        builder.Services.AddScoped(typeof(MyDBContext));
        builder.Services.AddScoped(typeof(UnitOfWork));
        builder.Services.AddScoped(typeof(ProductManager));
        builder.Services.AddScoped(typeof(CategoryManeger));
        builder.Services.AddControllersWithViews();

        var webApp = builder.Build();
        webApp.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory() + "/Content"),
            RequestPath = ""

        });
        webApp.MapControllerRoute("Default","{Controller=Home}/{Action=Index}/{id?}");
        webApp.Run();


        return 0;
    }
}