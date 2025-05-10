using System;
using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models;

public class BanDTO
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public UserDTO? AppUser { get; set; } = null!;
    [Required(ErrorMessage = "Reason is required.")]
    [StringLength(200, ErrorMessage = "Reason cannot be longer than 200 characters.")]
    public string? Reason { get; set; } 
    public DateTime BannedAt { get; set; } = DateTime.UtcNow; 
    public DateTime? BannedUntil { get; set; } 
    public bool IsPermanent { get; set; } = false;
}
