using System;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IRoleService
{
    Task<bool> CreateRoleAsync(string roleName);
    Task<bool> DeleteRoleAsync(int roleId);
    Task<bool> UpdateRoleAsync(int roleId, string newRoleName);
    Task<AppRole?> GetRoleByIdAsync(int roleId);
    Task<AppRole?> GetRoleByNameAsync(string roleName);
    Task<AppRole?> GetUserRoleByIdAsync(int userId);
    Task<string> GetUserRoleNameByIdAsync(int roleId);
    Task<bool> AddRoleToUserAsync(int userId, string roleName);
    Task<bool> RemoveRoleFromUserAsync(int userId, string roleName);
    Task<List<RoleDTO>?> GetUserRolesByIdAsync(int userId);
    Task<RoleListDTO> GetAllRolesAsync(int pageNumber, int pageSize = 10, string? search = null);
    Task<int> GetRoleCountAsync();
}
