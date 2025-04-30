using System;
using AutoMapper;
using LostAndFound.Data;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Identity;

namespace LostAndFound.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper, SignInManager<AppUser> signInManager, IPhotoService photoService) : IUserService
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

    public async Task<MemberDTO?> GetMemberByIdAsync(int id)
    {
        AppUser? user = await unitOfWork.UserRepository.GetUserDetailsByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        MemberDTO memberDTO = mapper.Map<MemberDTO>(user);
        return memberDTO;
    }

    public async Task<bool> UpdateMemberAsync(MemberDTO memberDto)
    {
        AppUser? user = await unitOfWork.UserRepository.GetUserDetailsByIdAsync(memberDto.Id);
        if (user == null)
        {
            return false;
        }

        if(memberDto.Photo != null)
        {
            // delete the current photo if it exists
            if (user.Photo != null)
            {
                var deleteResult = await photoService.DeletePhotoAsync(user.Photo.PublicId!);
                if (deleteResult == null)
                {
                    return false;
                }
                unitOfWork.PhotoRepository.DeletePhotoAsync(user.Photo);
                user.Photo = null;
            }

            var uploadResult = await photoService.UploadPhotoAsync(memberDto.Photo);
            if (uploadResult == null)
            {
                return false;
            }

        }

        mapper.Map(memberDto, user);
        unitOfWork.UserRepository.UpdateUser(user);
        var result = await unitOfWork.Complete();
        if (!result)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        await signInManager.SignOutAsync();
        unitOfWork.UserRepository.DeleteUser(id);
        var result = await unitOfWork.Complete();
        if (!result)
        {
            return false;
        }
        return true;
    }
}
