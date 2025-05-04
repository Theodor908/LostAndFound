using API.Extensions;
using LostAndFound.Data;
using LostAndFound.Entities;
using LostAndFound.Extensions;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using LostAndFound.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

using (var roleScope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    
    if (!context.Roles.Any())
    {
        var roles = new List<AppRole>
        {
            new AppRole { Name = "Admin", NormalizedName = "ADMIN" },
            new AppRole { Name = "Moderator", NormalizedName = "MODERATOR" },
            new AppRole { Name = "Member", NormalizedName = "MEMBER" }
        };
        
        await context.Roles.AddRangeAsync(roles);
        await context.SaveChangesAsync();
    }

    // create default admin user

    var authService = services.GetRequiredService<IAuthService>();
    var roleService = services.GetRequiredService<IRoleService>();
    var userService = services.GetRequiredService<IUserService>();

    var user = new RegisterDTO
    {
        Username = "admin9",
        Email = "admin9@admin.com",
        Password = "Admin123!",
        ConfirmPassword = "Admin123!",
        Country = "Adminland",
        City = "Admin City",
    };

    var (succeeded, errors, appUser) = await authService.RegisterAsync(user, true);

    if (succeeded)
    {
        var role = await roleService.GetRoleByNameAsync("Admin");
        Console.WriteLine($"Role: {role?.Name}");
        if (role != null)
        {
            var result = await roleService.AddRoleToUserAsync(appUser!.Id, "Admin");
            var userRole = await roleService.GetUserRolesByIdAsync(appUser.Id);
            Console.WriteLine($"User roles: {string.Join(", ", userRole!.Select(r => r.Name))}");
            if (result)
            {
                Console.WriteLine($"User {appUser.UserName} added to role {role.Name}.");
            }
            else
            {
                Console.WriteLine($"Failed to add user {appUser.UserName} to role {role.Name}.");
            }
        }
    }
    else
    {
        Console.WriteLine($"Failed to create admin user: {string.Join(", ", errors!.Select(e => e.Value))}");
    }
}

// try
// {
//     var context = services.GetRequiredService<DataContext>();
//     var userManager = services.GetRequiredService<UserManager<AppUser>>();
//     var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

//     await context.Database.MigrateAsync();

//     // await Seed.SeedUsers(userManager, roleManager);
//     // await Seed.SeedData(context); 
// }
// catch (Exception ex)
// {
//     var logger = services.GetRequiredService<ILogger<Program>>();
//     logger.LogError(ex, "An error occurred during migration");
// }


app.Run();
