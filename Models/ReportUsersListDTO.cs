using System;
using LostAndFound.Helpers;

namespace LostAndFound.Models;

public class ReportUsersListDTO
{
    public string? SearchTerm { get; set; } = string.Empty;
    public bool IsResolved { get; set; } = false;
    public PagedList<ReportUserDTO> ReportUsersList { get; set; } = new PagedList<ReportUserDTO>();
}
