using System;
using LostAndFound.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>(options) //specific order for this one
{
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Photo> Photos { get; set; } = null!;
    public DbSet<Item> Items { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {

        // Composite key for AppUserRole
        base.OnModelCreating(builder);

        // AppUser Role relationship
        builder.Entity<AppUser>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.User)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
        builder.Entity<AppRole>()
            .HasMany(ur => ur.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();
        // AppUser and Photo relationship
        builder.Entity<AppUser>()
            .HasOne(u => u.Photo)
            .WithOne(p => p.AppUser)
            .OnDelete(DeleteBehavior.Cascade);
        // AppUser and Item relationship
        builder.Entity<Item>()
            .HasOne(i => i.AppUser)
            .WithMany(u => u.Items)
            .OnDelete(DeleteBehavior.Cascade);
        // AppUser and Post relationship
        builder.Entity<Post>()
            .HasOne(p => p.AppUser)
            .WithMany(u => u.Posts)
            .OnDelete(DeleteBehavior.Cascade);
        // Category and Item relationship
        builder.Entity<Item>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Items)
            .OnDelete(DeleteBehavior.Cascade);

    }
}