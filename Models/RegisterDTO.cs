using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models
{
    public class RegisterDTO
    {
        [Required]
        [Display(Name = "Username")]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        public required string Email { get; set; }

        [Required]
        [Display(Name = "City")]
        public required string City { get; set; }

        [Required]
        [Display(Name = "Country")]
        public required string Country { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The password must be at least 4 characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public required string ConfirmPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; } = false;
    }
}
