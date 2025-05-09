using System;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using Microsoft.AspNetCore.Identity;

namespace LostAndFound.Interfaces;

public interface IUserRepository : IDisposable
{
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserDetailsByIdAsync(int id);
    Task<AppUser?> GetUserByEmailAsync(string email);
    Task<AppUser?> GetUserByUsernameAsync(string username);
    Task<AppUser?> GetUserDetailsByUsernameOrEmailAsync(string usernameOrEmail);
    Task<AppUser?> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
    Task<PagedList<AppUser>> GetAllUsersAsync(UserFilterParams userFilterParams);
    void UpdateUser(AppUser user);
    Task<IdentityResult> CreateUser(AppUser user, string password);
    bool VerifyPassword(AppUser user, string password);
    void DeleteUser(int id);
    Task<int> GetUserCountAsync();
    Task<List<string>> GetUserRolesByIdAsync(int userId);
}
