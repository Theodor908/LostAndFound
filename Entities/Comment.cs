using System;

namespace LostAndFound.Entities;

public class Comment
{

    public int Id { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int PostId { get; set; }
    public Post Post { get; set; } = null!;

    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

}
