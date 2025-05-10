using System;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data;

public class RoleRepository(DataContext dataContext) : IRoleRepository
{
    public async Task<AppRole?> GetRoleByIdAsync(int roleId)
    {
        return await dataContext.Roles.FindAsync(roleId);
    }

    public async Task<AppRole?> GetRoleByNameAsync(string roleName)
    {
        return await dataContext.Roles
            .FirstOrDefaultAsync(r => r.NormalizedName == roleName.ToUpper());
    }

    public async Task<bool> CreateRoleAsync(AppRole role)
    {
        try
        {
            role.NormalizedName = role.Name!.ToUpper();
            await dataContext.Roles.AddAsync(role);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<PagedList<AppRole>> GetAllRolesAsync(RoleFilterParams filterParams)
    {
        // use like for search term
        var query = dataContext.Roles.AsQueryable();
        if (!string.IsNullOrEmpty(filterParams.SearchTerm))
        {
            var searchTerm = filterParams.SearchTerm.ToLower();
            query = query.Where(r => r.Name!.ToLower().Contains(searchTerm.ToLower()));
        }
        query = query.OrderBy(r => r.Name);
        var roles = await query
            .Skip((filterParams.PageNumber - 1) * filterParams.PageSize)
            .Take(filterParams.PageSize)
            .ToListAsync();

        var result = new PagedList<AppRole>{
            Items = roles,
            CurrentPage = filterParams.PageNumber,
            PageSize = filterParams.PageSize,
            TotalCount = await dataContext.Users.CountAsync()
        };

        result.TotalPages = (int)Math.Ceiling((double)result.TotalCount / filterParams.PageSize);
        return result;
   
    }

    public async Task<bool> UpdateRoleAsync(AppRole role)
    {
        try
        {
            var existingRole = await GetRoleByIdAsync(role.Id);
            if (existingRole == null) return false;

            existingRole.Name = role.Name;
            existingRole.NormalizedName = role.Name!.ToUpper();
            dataContext.Roles.Update(existingRole);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteRoleAsync(AppRole role)
    {
        try
        {
            var existingRole = await GetRoleByIdAsync(role.Id);
            if (existingRole == null) return false;

            dataContext.Roles.Remove(existingRole);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<AppRole?> GetUserRoleAsync(int userId)
    {
        return await dataContext.UserRoles
            .Include(ur => ur.Role)
            .FirstOrDefaultAsync(ur => ur.UserId == userId)
            .ContinueWith(t => t.Result?.Role);
    }

    public async Task<bool> AddRoleToUser(int userId, string roleName)
    {
        var existingRole = dataContext.Roles
            .FirstOrDefault(r => r.Name == roleName);

        if (existingRole == null)
        {
            return false;
        }
            var userRole = new AppUserRole { UserId = userId, Role = existingRole };
            try
            {
                dataContext.UserRoles.Add(userRole);
                var user = await dataContext.Users.FindAsync(userId);
                if (user != null)
                {
                    user.UserRoles.Add(userRole);
                    dataContext.AppUserRole.Add(userRole);
                }
                return true;
            }
            catch
            {
                return false;
            }

    }

    public Task<bool> RemoveRoleFromUser(int userId, string roleName)
    {
        try
        {
            var userRole = dataContext.UserRoles
                .FirstOrDefault(ur => ur.UserId == userId && ur.Role.Name == roleName);
            if (userRole != null)
            {
                dataContext.UserRoles.Remove(userRole);
                var user = dataContext.Users.Find(userId);
                if (user != null)
                {
                    user.UserRoles.Remove(userRole);
                    dataContext.AppUserRole.Remove(userRole);
                }
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    public async Task<List<AppRole>?> GetUserRolesByIdAsync(int userId)
    {
        return await dataContext.UserRoles
            .Include(ur => ur.Role)
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role)
            .ToListAsync();
    }

    public async Task<int> GetRoleCountAsync()
    {
        return await dataContext.Roles.CountAsync();
    }

    public async Task<List<AppUser>?> GetUsersInRoleByIdAsync(int roleId)
    {
        return await dataContext.UserRoles
            .Include(ur => ur.User)
            .Where(ur => ur.RoleId == roleId)
            .Select(ur => ur.User)
            .ToListAsync();
    }

    public async Task<List<AppRole>?> GetAllRolesNoPaginationAsync()
    {
        return await dataContext.Roles.ToListAsync();
    }
}
