using System;

namespace LostAndFound.Models;

public class ItemDTO
{
    public required string Name { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public DateTime? FoundAt { get; set; } = null;
    public bool IsFound { get; set; } = false;
    public List<PhotoDTO> Photos { get; set; } = [];    
}
