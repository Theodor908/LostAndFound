using System;
using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
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

    public async Task<UserDTO?> GetUserByIdAsync(int id)
    {
        AppUser? user = await unitOfWork.UserRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        UserDTO userDTO = mapper.Map<UserDTO>(user);
        return userDTO;
    }

    public async Task<UserDTO?> GetUserByUsernameAsync(string username)
    {
        AppUser? user = await unitOfWork.UserRepository.GetUserByUsernameAsync(username);
        if (user == null)
        {
            return null;
        }
        UserDTO userDTO = mapper.Map<UserDTO>(user);
        return userDTO;
    }

    public void UpdateUser(UserDTO userDto)
    {
        throw new NotImplementedException();
    }
}
