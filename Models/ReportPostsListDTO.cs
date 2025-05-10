using System;
using LostAndFound.Helpers;

namespace LostAndFound.Models;

public class ReportPostsListDTO
{
    public string? SearchTerm { get; set; } = string.Empty;
    public bool IsResolved { get; set; } = false;
    public PagedList<ReportPostDTO> ReportPostsList { get; set; } = new PagedList<ReportPostDTO>();
}
