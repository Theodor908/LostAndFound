using System;

namespace LostAndFound.Helpers;

public class UserFilterParams
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; } = "Name"; 
    public bool? IsAscending { get; set; } = true; 
    public bool? IsBanned { get; set; } = false; 
}
