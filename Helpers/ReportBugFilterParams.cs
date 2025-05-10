using System;

namespace LostAndFound.Helpers;

public class ReportBugFilterParams
{
    public string? SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; } = true;
    public bool IsResolved { get; set; } = false;
}
