using System;
using LostAndFound.Entities;
using LostAndFound.Helpers;

namespace LostAndFound.Interfaces;

public interface IRoleRepository
{
    Task<AppRole?> GetRoleByIdAsync(int roleId);
    Task<AppRole?> GetRoleByNameAsync(string roleName);
    Task<bool> CreateRoleAsync(AppRole role);
    Task<PagedList<AppRole>> GetAllRolesAsync(RoleFilterParams filterParams);
    Task<bool> UpdateRoleAsync(AppRole role);
    Task<bool> DeleteRoleAsync(AppRole role);
    Task<AppRole?> GetUserRoleAsync(int userId);
    Task<bool> AddRoleToUser(int userId, string roleName);
    Task<bool> RemoveRoleFromUser(int userId, string roleName);
    Task<List<AppRole>?> GetUserRolesByIdAsync(int userId);
    Task<int> GetRoleCountAsync();
}
