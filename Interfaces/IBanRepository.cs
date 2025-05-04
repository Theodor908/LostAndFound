using System;
using LostAndFound.Entities;

namespace LostAndFound.Interfaces;

public interface IBanRepository
{
    Task<List<Ban>?> GetAllBansAsync();
    Task<Ban?> GetBanByIdAsync(int id);
    Task<Ban?> GetBanByUserIdAsync(int userId);
    void CreateBan(Ban ban);
    void UpdateBan(Ban ban);
    void DeleteBan(Ban ban);
    Task<int> GetBanCountAsync();
}
