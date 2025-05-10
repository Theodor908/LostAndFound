using System;

namespace LostAndFound.Models;

public class RoleDetailsDTO
{
    public int Id { get; set; } = 0;
    public string RoleName { get; set; } = string.Empty;
    public int UsersInRole { get; set; } = 0;
}
