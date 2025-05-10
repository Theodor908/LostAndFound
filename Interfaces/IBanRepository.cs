using System;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IBanRepository
{
    Task<PagedList<Ban>> GetAllBansAsync(BanFilterParams filterParams);
    Task<Ban?> GetBanByIdAsync(int id);
    Task<Ban?> GetBanByUserIdAsync(int userId);
    Task<string>? GetBanUserNameByIdAsync(int id);
    void CreateBan(Ban ban);
    void UpdateBan(Ban ban);
    void DeleteBan(Ban ban);
    Task<int> GetBanCountAsync();
    Task<bool> BanUserAsync(BanDTO banDTO);
    Task<bool> UnbanUserAsync(int userId);
    Task<Ban?> GetUserBanAsync(int userId);
}
