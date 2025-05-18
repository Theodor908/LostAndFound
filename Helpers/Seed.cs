using System.Text.Json;
using LostAndFound.Data;
using LostAndFound.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace LostAndFound.Helpers;

public static class Seed
{
    public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        if (await userManager.Users.AnyAsync()) return;

        var userData = await System.IO.File.ReadAllTextAsync("Helpers/Users.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);
        if (users == null) return;
        var roles = new List<AppRole>
        {
            new AppRole { Name = "Member" },
            new AppRole { Name = "Moderator" },
            new AppRole { Name = "Admin" }
        };
        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }

        foreach (var user in users)
        {
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRoleAsync(user, "Member");
        }

        var adminUser = new AppUser
        {
            UserName = "admin",
            Email = "admin@admin.com",
            City = "Admin City",
            Country = "Admin Country"
        };
        await userManager.CreateAsync(adminUser, "Pa$$w0rd");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }


    public static async Task SeedPosts(DataContext context)
    {
        if (await context.Posts.AnyAsync()) return;
        var postData = await System.IO.File.ReadAllTextAsync("Helpers/Posts.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var posts = JsonSerializer.Deserialize<List<Post>>(postData, options);
        if (posts == null) return;
        foreach (var post in posts)
        {
            context.Posts.Add(post);
        }
        await context.SaveChangesAsync();
    }

    public static async Task SeedCategories(DataContext context)
    {
        if (await context.Categories.AnyAsync()) return;
        var categoryData = await System.IO.File.ReadAllTextAsync("Helpers/Categories.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var categories = JsonSerializer.Deserialize<List<Category>>(categoryData, options);
        if (categories == null) return;
        foreach (var category in categories)
        {
            context.Categories.Add(category);
        }
        await context.SaveChangesAsync();

    }

    public static async Task SeedPhotos(DataContext context)
    {
        if (await context.Photos.AnyAsync()) return;
        var photoData = await System.IO.File.ReadAllTextAsync("Helpers/Photos.json");
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var photos = JsonSerializer.Deserialize<List<Photo>>(photoData, options);
        if (photos == null) return;
        foreach (var photo in photos)
        {
            context.Photos.Add(photo);
        }
        await context.SaveChangesAsync();
    }
    
}
