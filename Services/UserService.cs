using System;
using AutoMapper;
using LostAndFound.Data;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Identity;
//use claims here
using System.Security.Claims;

namespace LostAndFound.Services;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper, SignInManager<AppUser> signInManager, IPhotoService photoService, IRoleService roleService) : IUserService
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

    

    public async Task<MemberListDTO> GetAllUsersAsync(UserFilterParams userFilterParams)
    {
        var users = await unitOfWork.UserRepository.GetAllUsersAsync(userFilterParams);
        var usersDTO = mapper.Map<PagedList<MemberDTO>>(users);
        var memberListDTO = new MemberListDTO
        {
            SearchTerm = userFilterParams.SearchTerm,
            MemberList = usersDTO
        };
        return memberListDTO;
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

        mapper.Map(memberDto, user);

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
            user.Photo = new Photo
            {
                Url = uploadResult.Url.ToString(),
                PublicId = uploadResult.PublicId.ToString()
            };

        }

        unitOfWork.UserRepository.UpdateUser(user);
        var result = await unitOfWork.Complete();
        if (!result)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteUserAsync(int id, bool actionByAdmin = false)
    {
        if(!actionByAdmin)
            await signInManager.SignOutAsync();
        unitOfWork.UserRepository.DeleteUser(id);
        var result = await unitOfWork.Complete();
        if (!result)
        {
            return false;
        }
        return true;
    }

    public async Task<int> GetUserCountAsync()
    {
        return await unitOfWork.UserRepository.GetUserCountAsync();
    }

    public async Task<List<string>> GetUserRolesByIdAsync(int userId)
    {
        var user = await unitOfWork.UserRepository.GetUserByIdAsync(userId);
        if (user == null)
        {
            return new List<string>();
        }
        var roles = await unitOfWork.UserRepository.GetUserRolesByIdAsync(userId);
        return roles;
    }
}
