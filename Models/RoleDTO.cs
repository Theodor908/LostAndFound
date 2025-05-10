using System;
using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models;

public class RoleDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Role name is required")]
    [StringLength(50, ErrorMessage = "Role name cannot be longer than 50 characters")]
    public string Name { get; set; } = string.Empty;
}
