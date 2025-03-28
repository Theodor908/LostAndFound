using System;
using LostAndFound.Entities;

namespace LostAndFound.Models;

public class ItemBrowseViewModel
{
    public List<ItemDTO> Items { get; set; } = [];

    public Status Status;
    public CategoryDTO Category;    
    public string? Location;
    public string? DateRange;   

    public int CurrentPage;
    public int TotalPages;
    public int PageSize;
}
