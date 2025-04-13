using System;
using LostAndFound.Data;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Repositories;
using LostAndFound.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

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
    
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
    services.AddScoped<IPhotoService, PhotoService>();
    services.AddScoped<IPostService, PostService>();
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IItemRepository, ItemRepository>();
    services.AddScoped<IPostRepository, PostRepository>(); 
    services.AddScoped<ICategoryService, CategoryService>(); 
    services.AddScoped<IUserService, UserService>();  
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    return services;
}
}
