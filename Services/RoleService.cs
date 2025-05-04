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

    public async Task<RoleListDTO> GetAllRolesAsync(int pageNumber, int pageSize = 10, string? search = null)
    {
        var roles =  await unitOfWork.RoleRepository.GetAllRolesAsync(pageNumber, pageSize, search);
        var roleList = mapper.Map<PagedList<RoleDTO>>(roles);
        RoleListDTO roleListDTO = new()
        {
            SearchTerm = search,
            RoleList = roleList,
        };
        return roleListDTO;
    }

    public async Task<RoleDTO> GetRoleByIdAsync(int roleId)
    {
        var role = await unitOfWork.RoleRepository.GetRoleByIdAsync(roleId);
        var roleDTO = mapper.Map<RoleDTO>(role);
        return roleDTO;

    }

    public Task<AppRole?> GetRoleByNameAsync(string roleName)
    {
        return unitOfWork.RoleRepository.GetRoleByNameAsync(roleName);
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

    public Task<AppRole?> GetUserRoleByIdAsync(int userId)
    {
        return unitOfWork.RoleRepository.GetUserRoleAsync(userId);
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

    public async Task<List<AppRole>?> GetUserRolesByIdAsync(int userId)
    {
        return await unitOfWork.RoleRepository.GetUserRolesByIdAsync(userId);
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
}
