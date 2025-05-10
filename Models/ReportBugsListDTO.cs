using System;
using LostAndFound.Helpers;

namespace LostAndFound.Models;

public class ReportBugsListDTO
{
    public string? SearchTerm { get; set; } = string.Empty;
    public bool IsResolved { get; set; } = false;
    public PagedList<ReportBugDTO> ReportBugsList { get; set; } = new PagedList<ReportBugDTO>();
}
