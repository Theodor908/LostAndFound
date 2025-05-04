using System;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IBanService
{
    Task<bool> BanUserAsync(string username, string reason);
    Task<bool> UnbanUserAsync(string username);
    Task<List<MemberDTO>?> GetBannedUsersAsync();
    Task<bool> IsUserBannedAsync(string username);
    Task<string?> GetBanReasonAsync(string username);
    Task<DateTime?> GetBanEndDateAsync(string username);
    Task<int> GetBanCountAsync();
}
