using System;

namespace LostAndFound.Entities;

public class Item
{

    public int Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required string Country { get; set; } = string.Empty;
    public required string City { get; set; } = string.Empty;
    public required string Location { get; set; } = string.Empty;
    public DateTime PostedAt { get; set; } = DateTime.UtcNow;
    public DateTime? FoundAt { get; set; } = null;
    public DateTime? LostAt { get; set; } = null;
    public bool IsFound { get; set; } = false;
    public bool IsClaimed { get; set; } = false;
    public List<Photo> Photos { get; set; } = [];    
    // nav props
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    public int PostId { get; set; }
    public Post Post { get; set; } = null!;

}
