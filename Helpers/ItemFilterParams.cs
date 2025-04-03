using LostAndFound.Entities;

public class ItemFilterParams
{
    public string? Status { get; set; } 
    public string? CategoryName { get; set; } 
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? DateRange { get; set; } 
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = true;
}