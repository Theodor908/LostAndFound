using System;
using LostAndFound.Helpers;

namespace LostAndFound.Models;

public class BanListDTO
{
    public string? SearchTerm { get; set; } = string.Empty;
    public PagedList<BanDTO> BannedUsersList { get; set; } = new PagedList<BanDTO>();
    public int TotalBannedUsers { get; set; } = 0;
}
