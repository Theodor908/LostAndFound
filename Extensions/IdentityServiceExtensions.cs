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
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

    services.AddAuthorizationBuilder()
        .AddPolicy("ModerateRoles", policy => policy.RequireRole("Admin"))
        .AddPolicy("ModeratePostAndUser", policy => policy.RequireRole("Admin", "Moderator"))
        .AddPolicy("ViewPostDetails", policy => policy.RequireRole("Admin", "Member"));

    return services;
}
}