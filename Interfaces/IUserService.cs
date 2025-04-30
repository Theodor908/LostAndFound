using System;
using LostAndFound.Entities;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IUserService
{
    Task<AppUser?> FindByEmailAsync(string email);
    Task<AppUser?> FindByNameAsync(string username);
    Task<UserDTO?> GetUserByIdAsync(int id);
    Task<MemberDTO?> GetMemberByIdAsync(int id);
    Task<UserDTO?> GetUserByEmailAsync(string email);
    Task<UserDTO?> GetUserByUsernameAsync(string username);
    Task<List<UserDTO>?> GetAllUsersAsync();
    Task<bool> UpdateMemberAsync(MemberDTO memberDto);
    Task<bool> DeleteUserAsync(int id);
}
