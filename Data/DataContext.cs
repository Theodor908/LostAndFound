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
    public DbSet<AppUserRole> AppUserRole { get; set; } = null!;
    public DbSet<ReportPost> ReportPosts { get; set; } = null!;
    public DbSet<ReportUser> ReportUsers { get; set; } = null!;
    public DbSet<ReportBug> ReportBugs { get; set; } = null!;
    public DbSet<Ban> Bans { get; set; } = null!;
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
            .HasForeignKey<Photo>(p => p.AppUserId)
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
        // Item and Photo relationship
        builder.Entity<Item>()
            .HasMany(i => i.Photos)
            .WithOne(p => p.Item)
            .HasForeignKey(p => p.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Ban>()
            .HasOne(b => b.AppUser)
            .WithMany(u => u.Bans)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ReportPost>()
            .HasOne(rp => rp.ReportedByUser)
            .WithMany(u => u.ReportPosts)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<ReportUser>()
            .HasOne(ru => ru.ReportedByUser)
            .WithMany(u => u.ReportUsers)
            .OnDelete(DeleteBehavior.Cascade);

    }
}