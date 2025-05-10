using System;

namespace LostAndFound.Helpers;

public class BanFilterParams
{
    public string? SearchTerm { get; set; } = string.Empty;
    public DateTime? BannedFrom { get; set; } = null;
    public bool IsPermanenet { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool SortDescending { get; set; } = true;
}
