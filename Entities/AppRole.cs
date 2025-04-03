using System;
using Microsoft.AspNetCore.Identity;

namespace LostAndFound.Entities;

public class AppRole:IdentityRole<int>
{
    public List<AppUserRole> UserRoles { get; set; } = [];
}
