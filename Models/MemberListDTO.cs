using System;
using LostAndFound.Helpers;

namespace LostAndFound.Models;

public class MemberListDTO
{  
    public string? SearchTerm { get; set; } = string.Empty;
    public PagedList<MemberDTO> MemberList { get; set; } = new PagedList<MemberDTO>();

}
