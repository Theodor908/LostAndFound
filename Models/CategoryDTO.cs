using System;

namespace LostAndFound.Models;

public class CategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int ItemCount { get; set; }
    public List<ItemDTO> Items { get; set; } = [];
}
