using System;
using LostAndFound.Helpers;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IBanService
{
    Task<bool> BanUserAsync(BanDTO banDTO);
    Task<bool> UnbanUserAsync(int userId);
    Task<BanDTO?> GetBanByIdAsync(int id);
    Task<BanListDTO> GetBannedUsersAsync(BanFilterParams filterParams);
    Task<bool> IsUserBannedAsync(string username);
    Task<string?> GetBanReasonAsync(string username);
    Task<DateTime?> GetBanEndDateAsync(string username);
    Task<int> GetBanCountAsync();
    Task<BanDTO?> GetUserBanAsync(int userId);
    Task<bool> DeleteBanByIdAsync(int id);
}
