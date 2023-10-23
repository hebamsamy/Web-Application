using API.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repository;
using Repository.Helpers;
using System.Text;

public class Program
{
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
        builder.Services.AddIdentity<User, IdentityRole>(i => {
            i.Lockout.MaxFailedAccessAttempts = 2;
            i.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            i.User.RequireUniqueEmail = true;
            i.SignIn.RequireConfirmedPhoneNumber = false;
            i.SignIn.RequireConfirmedEmail = false;
            i.SignIn.RequireConfirmedAccount = false;
            i.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
        })
            .AddEntityFrameworkStores<MyDBContext>()
            .AddDefaultTokenProviders();
        builder.Services.Configure<IdentityOptions>(i =>
        {
            i.Password.RequireNonAlphanumeric = false;
            i.Password.RequireUppercase = false;

        });
        builder.Services.ConfigureApplicationCookie(i =>
        {
            i.LoginPath = "/Account/SignIn";

        });
        builder.Services.AddScoped(typeof(UnitOfWork));
        builder.Services.AddScoped(typeof(ProductManager));
        builder.Services.AddScoped(typeof(CategoryManeger));
        builder.Services.AddScoped(typeof(AccountManger));
        builder.Services.AddScoped(typeof(RoleManager));
        builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, UesrClaimsFactory>();
        
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ExceptionHandler>();
        }).AddNewtonsoftJson();

        builder.Services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(option =>
        {
            option.SaveToken = true;
            option.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"]!))
            };
        });
        builder.Services.AddCors(option =>
        {
            option.AddDefaultPolicy(i =>
            i.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
        });
        #endregion

        var webApp = builder.Build();

        #region Middel Were
        webApp.UseStaticFiles(new StaticFileOptions()
        {
            FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory() + "/Content"),
            RequestPath = ""

        });
        webApp.UseRouting();
        webApp.UseCors();
        webApp.UseAuthentication();
        webApp.UseAuthorization();
        webApp.MapControllerRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");

        #endregion

        webApp.Run();


        return 0;
    }
}