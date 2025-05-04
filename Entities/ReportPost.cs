using System;

namespace LostAndFound.Entities;

public class ReportPost
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int PostId { get; set; }
    public Post? Post { get; set; }
    public int ReportedUserId { get; set; }
    public int ReportedByUserId { get; set; }
    public AppUser? ReportedByUser { get; set; } = null!;
    public bool IsResolved { get; set; } = false;
}
