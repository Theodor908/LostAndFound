using System;
using AutoMapper;
using LostAndFound.Data;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper) : IUserService
{

    public async Task<AppUser?> FindByEmailAsync(string email)
    {
        AppUser? user = await unitOfWork.UserRepository.GetUserByEmailAsync(email);
        return user;
    }

    public async Task<AppUser?> FindByNameAsync(string username)
    {
        AppUser? user = await unitOfWork.UserRepository.GetUserByUsernameAsync(username);
        return user;
    }

    public void CreateUser(AppUser user, string password)
    {
        unitOfWork.UserRepository.CreateUser(user, password);;
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

    public void CreateUser(UserDTO userDto)
    {
        throw new NotImplementedException();
    }
}
