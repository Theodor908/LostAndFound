using System;
using LostAndFound.Entities;

namespace LostAndFound.Interfaces;

public interface IUserRepository
{
    void UpdateUser(AppUser user);
    Task<AppUser?> GetUserByIdAsync(int id);
    Task<AppUser?> GetUserByUsernameAsync(string username);
    Task<AppUser?> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
}
