using System;
using LostAndFound.Data;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Repositories;
using LostAndFound.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

namespace LostAndFound.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
{
    services.AddControllers();
    services.AddDbContext<DataContext>(options =>
    {
        options.UseSqlite(config.GetConnectionString("DefaultConnection"));
    });

    services.AddAuthentication(options => 
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

    services.AddHybridCache(options =>
    {
        options.DefaultEntryOptions = new HybridCacheEntryOptions
        {
            LocalCacheExpiration = TimeSpan.FromMinutes(5),
            Expiration = TimeSpan.FromMinutes(30),
        };
    });
    
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));

    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddSingleton<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();

    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IItemRepository, ItemRepository>();
    services.AddScoped<IPostRepository, PostRepository>();
    services.AddScoped<IPhotoRepository, PhotoRepository>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<IRoleRepository, RoleRepository>();
    services.AddScoped<IReportRepository, ReportRepository>();
    services.AddScoped<IBanRepository, BanRepository>();

    services.AddScoped<IUnitOfWork, UnitOfWork>();

    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped<IPhotoService, PhotoService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IPostService, PostService>();
    services.AddScoped<IReportService, ReportService>();
    services.AddScoped<ICategoryService, CategoryService>();
    services.AddScoped<IItemService, ItemService>();
    services.AddScoped<IBrowseService, BrowseService>();
    services.AddScoped<IBanService, BanService>();
    services.AddScoped<IAdminService, AdminService>();


    services.AddScoped<IAuthService, AuthService>();
    return services;
}
}
