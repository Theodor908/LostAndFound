using System;
using System.ComponentModel.DataAnnotations;
using LostAndFound.Helpers.Validators;

namespace LostAndFound.Models;

public class MemberDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Username is required")]
    [Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "City is required")]
    [Display(Name = "City")]
    public string City { get; set; } = string.Empty;
    [Required(ErrorMessage = "Country is required")]
    [Display(Name = "Country")]
    public string Country { get; set; } = string.Empty;
    public PhotoDTO? PhotoDTO { get; set; } = null;
    [Display(Name = "Photo")]
    [MaxFileSize(1024 * 1024, ErrorMessage = "Maximum file size is 1MB")]
    [AllowedExtensions([".jpg", ".jpeg", ".png", ".gif"], ErrorMessage = "Only image files (.jpg, .jpeg, .png, .gif) are allowed")]
    public IFormFile? Photo { get; set; }
    public bool IsBanned { get; set; } = false;
}
