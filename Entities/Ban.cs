using System;

namespace LostAndFound.Entities;

public class Ban
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public AppUser? AppUser { get; set; } = null!;
    public string? Reason { get; set; } 
    public DateTime BannedAt { get; set; } = DateTime.UtcNow; 
    public DateTime? BannedUntil { get; set; } 
    public bool IsPermanent { get; set; } = false; 
}
