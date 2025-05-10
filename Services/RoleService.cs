using System;
using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class RoleService(IUnitOfWork unitOfWork, IMapper mapper) : IRoleService
{
    public async Task<bool> CreateRoleAsync(string roleName)
    {
        var role = new AppRole { Name = roleName };
        await unitOfWork.RoleRepository.CreateRoleAsync(role);
        var result = await unitOfWork.Complete();
        return result;
    }

    public async Task<bool> DeleteRoleAsync(int roleId)
    {
        var role = await unitOfWork.RoleRepository.GetRoleByIdAsync(roleId);
        if (role == null)
        {
            return false;
        }
        await unitOfWork.RoleRepository.DeleteRoleAsync(role);
        var result = await unitOfWork.Complete();
        return result;
    }

    public async Task<RoleListDTO> GetAllRolesAsync(RoleFilterParams roleFilterParams)
    {
        var roles =  await unitOfWork.RoleRepository.GetAllRolesAsync(roleFilterParams);
        var roleList = mapper.Map<PagedList<RoleDTO>>(roles);
        RoleListDTO roleListDTO = new()
        {
            SearchTerm = roleFilterParams.SearchTerm,
            RoleList = roleList,
        };
        return roleListDTO;
    }

    public async Task<RoleDTO?> GetRoleByIdAsync(int roleId)
    {
        var role = await unitOfWork.RoleRepository.GetRoleByIdAsync(roleId);
        var roleDTO = mapper.Map<RoleDTO>(role);
        return roleDTO;

    }

    public async Task<RoleDTO?> GetRoleByNameAsync(string roleName)
    {
        var role = await unitOfWork.RoleRepository.GetRoleByNameAsync(roleName);
        var roleDTO = mapper.Map<RoleDTO>(role);
        return roleDTO;
    }

    public async Task<bool> UpdateRoleAsync(int roleId, string newRoleName)
    {
        var role = await unitOfWork.RoleRepository.GetRoleByIdAsync(roleId);
        if (role == null)
        {
            return false;
        }
        role.Name = newRoleName;
        await unitOfWork.RoleRepository.UpdateRoleAsync(role);
        var result = await unitOfWork.Complete();
        return result;
    }

    public async Task<RoleDTO?> GetUserRoleByIdAsync(int userId)
    {
        var role = await unitOfWork.RoleRepository.GetUserRoleAsync(userId);
        var roleDTO = mapper.Map<RoleDTO>(role);
        return roleDTO;
    }
    
    public async Task<string> GetUserRoleNameByIdAsync(int roleId)
    {
        var role = await unitOfWork.RoleRepository.GetUserRoleAsync(roleId);
        if (role == null)
        {
            return string.Empty;
        }
        return role.Name!;
    }

    public async Task<List<RoleDTO>?> GetUserRolesByIdAsync(int userId)
    {
        var roles = await unitOfWork.RoleRepository.GetUserRolesByIdAsync(userId);
        var roleDTOs = mapper.Map<List<RoleDTO>>(roles);
        return roleDTOs;
    }

    public async Task<bool> AddRoleToUserAsync(int userId, string roleName)
    {
        await unitOfWork.RoleRepository.AddRoleToUser(userId, roleName);
        var result = await unitOfWork.Complete();
        return result;

    }

    public async Task<bool> RemoveRoleFromUserAsync(int userId, string roleName)
    {
        await unitOfWork.RoleRepository.RemoveRoleFromUser(userId, roleName);
        var result = await unitOfWork.Complete();
        return result;
    }

    public Task<int> GetRoleCountAsync()
    {
        return unitOfWork.RoleRepository.GetRoleCountAsync();
    }

    public async Task<List<UserDTO>?> GetUsersInRoleByIdAsync(int roleId)
    {
        var users = await unitOfWork.RoleRepository.GetUsersInRoleByIdAsync(roleId);
        var userDTOs = mapper.Map<List<UserDTO>>(users);
        return userDTOs;
    }

    public async Task<List<RoleAssignmentDTO>?> GetUserRoleAssignmentByIdAsync(int userId)
    {
        var userRoles = await unitOfWork.RoleRepository.GetUserRolesByIdAsync(userId);
        if (userRoles == null)
        {
            return null;
        }
        var allRoles = await unitOfWork.RoleRepository.GetAllRolesNoPaginationAsync();
        if (allRoles == null)
        {
            return null;
        }
        var roleAssignments = new List<RoleAssignmentDTO>();
        foreach (var role in allRoles)
        {
            var isAssigned = userRoles.Any(r => r.Id == role.Id);
            var roleAssignment = new RoleAssignmentDTO
            {
                Role = mapper.Map<RoleDTO>(role),
                IsAssigned = isAssigned
            };
            roleAssignments.Add(roleAssignment);
        }
        return roleAssignments;
    }

    public async Task<bool> UpdateUserRoleAssignmentAsync(int userId, List<RoleAssignmentDTO> roleAssignments)
    {
        List<AppRole>? userRole = await unitOfWork.RoleRepository.GetUserRolesByIdAsync(userId);
        if (userRole!.Count == 0)
        {
            foreach (var roleAssignment in roleAssignments)
            {
                if (roleAssignment.IsAssigned)
                {
                    await unitOfWork.RoleRepository.AddRoleToUser(userId, roleAssignment.Role.Name!);
                }
            }
        }
        else
        {
            foreach (var roleAssignment in roleAssignments)
            {
                if (roleAssignment.IsAssigned && !userRole.Any(r => r.Id == roleAssignment.Role.Id))
                {
                    await unitOfWork.RoleRepository.AddRoleToUser(userId, roleAssignment.Role.Name!);
                }
            }
        }
        var result = await unitOfWork.Complete();
        return result;
    }
}
