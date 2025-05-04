using System;
using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models;

public class ReportUserDTO
{
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    [DataType(DataType.Text)]
    [Display(Name = "Title")]
    [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [StringLength(500)]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Description")]
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int ReportedUserId { get; set; }
    public UserDTO? ReportedUser { get; set; }
    public int ReportedByUserId { get; set; }
    public UserDTO? ReportedByUser { get; set; }
    public bool IsResolved { get; set; } = false;

}
