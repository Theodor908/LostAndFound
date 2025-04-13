using System;

namespace LostAndFound.Entities;

public class Post
{
    public int Id { get; set; }
    public bool IsClosed { get; set; } = false;
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<Item> Items { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    public bool IsActive { get; set; } = true;

    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
}
