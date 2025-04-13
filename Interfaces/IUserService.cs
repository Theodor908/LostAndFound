using System;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IUserService
{
    Task<UserDTO?> GetUserByIdAsync(int id);
    Task<UserDTO?> GetUserByEmailAsync(string email);
    Task<UserDTO?> GetUserByUsernameAsync(string username);
    Task<List<UserDTO>?> GetAllUsersAsync();
    void UpdateUser(UserDTO userDto);
    void DeleteUser(int id);
    void CreateUser(UserDTO userDto);
}
