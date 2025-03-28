using System;
using System.Reflection.Metadata;

namespace LostAndFound.Entities;

public class FoundItem
{
    public int Id;
    public string Name = string.Empty;
    public string? Description;
    public Status Status;
    public List<Photo> Photos;
    // Category nav
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    // User nav
    public int UserId { get; set; }
    public AppUser User { get; set; } = null!;

}
