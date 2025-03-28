using System;
using Microsoft.AspNetCore.Identity;

namespace LostAndFound.Entities;

public class AppUser:IdentityUser<int>
{
    public List<Item> Items { get; set; } = [];
}
