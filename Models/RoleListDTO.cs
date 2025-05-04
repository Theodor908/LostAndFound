using System;
using LostAndFound.Helpers;

namespace LostAndFound.Models;

public class RoleListDTO
{
    public string? SearchTerm { get; set; } = string.Empty;
    public PagedList<RoleDTO> RoleList { get; set; } = new PagedList<RoleDTO>();
}
