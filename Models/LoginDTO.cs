using System;
using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models;

public class LoginDTO
{
    [Required]
    [Display(Name = "Username or email")]
    public required string UsernameOrEmail { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public required string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; } = false;
}