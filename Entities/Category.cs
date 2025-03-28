using System;

namespace LostAndFound.Entities;

public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int ItemCount { get; set; }
    public List<LostItem> LostItems { get; set; } = [];
    public List<FoundItem> FoundItems { get; set; } = [];

}
