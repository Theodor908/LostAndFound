// using LostAndFound.Data;
// using LostAndFound.Entities;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// namespace LostAndFound.Helpers;

// public static class Seed
// {
//     public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
//     {
//         if (await userManager.Users.AnyAsync()) return; // Prevent duplicate seeding

//         // Seed Roles
//         var roles = new List<AppRole>
//         {
//             new AppRole { Name = "User" },
//             new AppRole { Name = "Admin" }
//         };

//         foreach (var role in roles)
//         {
//             await roleManager.CreateAsync(role);
//         }

//         // Seed Users
//         var users = new List<AppUser>
//         {
//             new AppUser { UserName = "john_doe", Email = "john@example.com", City = "New York", Country = "USA" },
//             new AppUser { UserName = "jane_smith", Email = "jane@example.com", City = "London", Country = "UK" }
//         };

//         foreach (var user in users)
//         {
//             await userManager.CreateAsync(user, "Password123!"); // You can set any password here
//             await userManager.AddToRoleAsync(user, "User"); // Assign the "User" role
//         }

//         // Seed Admin User
//         var admin = new AppUser
//         {
//             UserName = "admin",
//             Email = "admin@example.com",
//             City = "San Francisco",
//             Country = "USA"
//         };

//         await userManager.CreateAsync(admin, "AdminPass123!"); // Admin password
//         await userManager.AddToRoleAsync(admin, "Admin"); // Assign the "Admin" role
//     }

//     public static async Task SeedData(DataContext context)
//     {
//         if (await context.Categories.AnyAsync()) return;

//         // Seed Categories
//         var categories = new List<Category>
//         {
//             new Category { Id = 1, Name = "Electronics", ItemCount = 0 },
//             new Category { Id = 2, Name = "Clothing", ItemCount = 0 },
//             new Category { Id = 3, Name = "Documents", ItemCount = 0 }
//         };

//         await context.Categories.AddRangeAsync(categories);
//         await context.SaveChangesAsync(); // Save categories first to ensure they have IDs

//         // Seed Posts
//         var posts = new List<Post>
//         {
//             new Post { Title = "Found iPhone", Description = "Found an iPhone in Central Park", AppUserId = 1 }, // Make sure AppUserId matches seeded user
//             new Post { Title = "Lost Wallet", Description = "Lost wallet on Oxford Street", AppUserId = 2 } // Make sure AppUserId matches seeded user
//         };

//         await context.Posts.AddRangeAsync(posts);
//         await context.SaveChangesAsync(); // Save posts first to ensure they have IDs

//         // Seed Items (with correct foreign keys)
//         var items = new List<Item>
//         {
//             new Item
//             {
//                 Id = 1, Name = "iPhone 13", Description = "Lost at Central Park",
//                 Country = "USA", City = "New York", SpecificLocation = "Near the fountain",
//                 IsFound = false, CategoryId = 1, AppUserId = 1, PostId = posts[0].Id // Reference the correct Post
//             },
//             new Item
//             {
//                 Id = 2, Name = "Wallet", Description = "Brown leather wallet",
//                 Country = "UK", City = "London", SpecificLocation = "Oxford Street",
//                 IsFound = true, CategoryId = 3, AppUserId = 2, PostId = posts[1].Id // Reference the correct Post
//             }
//         };

//         await context.Items.AddRangeAsync(items);
//         await context.SaveChangesAsync(); // Save items after posts and categories

//         // Seed Photos
//         var photos = new List<Photo>
//         {
//             new Photo { Id = 1, Url = "https://example.com/photo1.jpg", AppUserId = 1, ItemId = 1 },
//             new Photo { Id = 2, Url = "https://example.com/photo2.jpg", AppUserId = 2, ItemId = 2 }
//         };

//         await context.Photos.AddRangeAsync(photos);
//         await context.SaveChangesAsync();
//     }
// }
