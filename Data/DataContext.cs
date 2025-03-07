using System;
using LostAndFound.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data;

public class DataContext(DbContextOptions options): IdentityDbContext<AppUser>(options)
{
    DbSet<AppUser> Users { get; set; }

    
}
