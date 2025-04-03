using System;
using LostAndFound.Entities;
using LostAndFound.Helpers;

namespace LostAndFound.Models;

public class BrowseDTO
{
    public string? ItemStatus = string.Empty;
    public string? CategoryName { get; set; }
    public List<string> Countries { get; set; } = [];
    public string? Country { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
    public List<string> Cities { get; set; } = [];
    public string? DateRange { get; set; } = string.Empty;
    public string? SearchTerm { get; set; } = string.Empty;
    public PagedList<Item> Items { get; set; } = new();
    public List<Category> Categories { get; set; } = [];
    public ItemFilterParams FilterParams { get; set; } = new();
}