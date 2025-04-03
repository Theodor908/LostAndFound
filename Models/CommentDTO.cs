using System;

namespace LostAndFound.Models;

public class CommentDTO
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
