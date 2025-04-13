using System;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class UserService : IUserService
{
    public void CreateUser(UserDTO userDto)
    {
        throw new NotImplementedException();
    }

    public void DeleteUser(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserDTO>?> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO?> GetUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public void UpdateUser(UserDTO userDto)
    {
        throw new NotImplementedException();
    }
}
