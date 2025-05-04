using System;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IAdminService
{

    public Task<AdminDTO?> GetAdminDashboardDataAsync();
    public Task<bool> BanUserAsync(int userId, string reason, DateTime? banEndDate = null, bool deleteAccount = false);
    public Task<bool> UnbanUserAsync(int userId);
    public Task<bool> AddRoleToUserAsync(int userId, string roleName);
    public Task<bool> RemoveRoleFromUserAsync(int userId, string roleName);

}
