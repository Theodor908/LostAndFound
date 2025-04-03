namespace API.Extensions;

using LostAndFound.Data;
using LostAndFound.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
{
    services.AddIdentityCore<AppUser>(opt =>
    {
        opt.Password.RequireDigit = false;
        opt.Password.RequiredLength = 4;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequireLowercase = false;
    })
    .AddRoles<AppRole>()
    .AddRoleManager<RoleManager<AppRole>>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

    services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

    services.AddAuthorizationBuilder()
        .AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"))
        .AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));

    return services;
}
}