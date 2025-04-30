using System;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data;

public class UserRepository(DataContext dataContext, IPasswordHasher<AppUser> passwordHasher) : IUserRepository
{
    private bool _disposed = false;
    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await dataContext.Users
            .Include(u => u.Posts)
            .Include(u => u.Items).ThenInclude(i => i.Photos)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<AppUser?> GetUserDetailsByIdAsync(int id)
    {
        return await dataContext.Users
            .Include(u => u.Photo)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<AppUser?> GetUserDetailsByUsernameOrEmailAsync(string usernameOrEmail)
    {
        return await dataContext.Users
            .Include(u => u.Photo)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.UserName == usernameOrEmail || u.Email == usernameOrEmail);
    }

    public async Task<AppUser?> GetUserByEmailAsync(string email)
    {
        return await dataContext.Users
            .Include(u => u.Items)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
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

    public async Task<IdentityResult> CreateUser(AppUser user, string password)
    {
        ThrowIfDisposed();
        
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

        try
        {
            if (await dataContext.Users.AnyAsync(u => u.UserName == user.UserName))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Username already exists." });
            }

            if (await dataContext.Users.AnyAsync(u => u.Email == user.Email))
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email already exists." });
            }

            user.PasswordHash = passwordHasher.HashPassword(user, password);
            
            await dataContext.Users.AddAsync(user);
            
            return IdentityResult.Success;
        }
        catch (Exception ex)
        {
            return IdentityResult.Failed(new IdentityError { Description = ex.Message });
        }
    }


    public void DeleteUser(int id)
    {
        var user = dataContext.Users.Find(id);
        if (user != null)
        {
            dataContext.Users.Remove(user);
        }
    }

    public bool VerifyPassword(AppUser user, string password)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

        return passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;
    }

    public void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(UserRepository));
        }
    }

    public void Dispose()
    {
        if(!_disposed)
        {
            dataContext.Dispose();
            _disposed = true;
        }
    }
}
