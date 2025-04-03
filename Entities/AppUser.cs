using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LostAndFound.Entities;

public class AppUser:IdentityUser<int>
{
    public required string City { get; set; } = string.Empty;
    public required string Country { get; set; } = string.Empty;
    public List<Item> Items = [];
    public List<AppUserRole> UserRoles { get; set; } = [];
    public Photo? Photo { get; set; } = null!;

    public List<Post> Posts { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
}
