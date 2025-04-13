using System;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data;

public class UserRepository(DataContext dataContext) : IUserRepository
{
    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await dataContext.Users
            .Include(u => u.Items)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await dataContext.Users
            .Include(u => u.Items)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<AppUser?> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
    {
        return await dataContext.Users
            .Include(u => u.Items)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserName == usernameOrEmail || u.Email == usernameOrEmail);
    }
    
    public void UpdateUser(AppUser user)
    {
        dataContext.Entry(user).State = EntityState.Modified;
    }
}
