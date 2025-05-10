using System;
using LostAndFound.Helpers;

namespace LostAndFound.Models;

public class MemberListDTO
{  
    public string? SearchTerm { get; set; } = string.Empty;
    public string? SortBy { get; set; } = "Name";
    public bool IsBanned { get; set; } = false;
    public PagedList<MemberDTO> MemberList { get; set; } = new PagedList<MemberDTO>();

}
