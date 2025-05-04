using System;

namespace LostAndFound.Entities;

public class Ban
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public string? Reason { get; set; } 
    public DateTime BannedAt { get; set; } = DateTime.UtcNow; 
    public DateTime? UnbannedAt { get; set; } 
    public bool IsPermanent { get; set; } = false; 
}
