using System;

namespace LostAndFound.Models;

public class PostDTO
{

    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public List<ItemDTO> Items { get; set; } = [];
    public List<CommentDTO> Comments { get; set; } = [];

}
