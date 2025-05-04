using System;
using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Helpers;
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
    Task<MemberListDTO> GetAllUsersAsync(UserFilterParams userFilterParams);
    Task<bool> UpdateMemberAsync(MemberDTO memberDto);
    Task<bool> DeleteUserAsync(int id);
    Task<int> GetUserCountAsync();
    Task<int> GetUserReportCountAsync();
    Task<List<string>> GetUserRolesByIdAsync(int userId);
}
