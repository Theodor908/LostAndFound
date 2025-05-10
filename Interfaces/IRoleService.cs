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
    Task<RoleDTO?> GetRoleByIdAsync(int roleId);
    Task<RoleDTO?> GetRoleByNameAsync(string roleName);
    Task<RoleDTO?> GetUserRoleByIdAsync(int userId);
    Task<string> GetUserRoleNameByIdAsync(int roleId);
    Task<bool> AddRoleToUserAsync(int userId, string roleName);
    Task<bool> RemoveRoleFromUserAsync(int userId, string roleName);
    Task<List<RoleDTO>?> GetUserRolesByIdAsync(int userId);
    Task<RoleListDTO> GetAllRolesAsync(RoleFilterParams roleFilterParams);
    Task<List<UserDTO>?> GetUsersInRoleByIdAsync(int roleId);
    Task<int> GetRoleCountAsync();
    Task<List<RoleAssignmentDTO>?> GetUserRoleAssignmentByIdAsync(int userId);
    Task<bool> UpdateUserRoleAssignmentAsync(int userId, List<RoleAssignmentDTO> roleAssignments);

}
