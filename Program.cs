using API.Extensions;
using LostAndFound.Data;
using LostAndFound.Entities;
using LostAndFound.Extensions;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
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
