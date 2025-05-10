using System;
using System.ComponentModel.DataAnnotations;

namespace LostAndFound.Models;

public class CategoryDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Category name is required")]
    [StringLength(50, ErrorMessage = "Category name cannot be longer than 50 characters")]
    public string Name { get; set; } = null!;
    public int ItemCount { get; set; }
    public List<ItemDTO> Items { get; set; } = [];
}
