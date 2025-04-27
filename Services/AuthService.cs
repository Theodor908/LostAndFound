using System;
using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Services;

public class AuthService(IUnitOfWork unitOfWork, SignInManager<AppUser> signInManager) : IAuthService
{

    public async Task<(bool Succeeded, Dictionary<string, string>? Errors, AppUser? User)> RegisterAsync(RegisterDTO registerDTO)
    {

        var existingUser = await unitOfWork.UserRepository.GetUserByUsernameAsync(registerDTO.Username);
        if (existingUser != null)
        {
            return (false, new Dictionary<string, string> { { "Username", "Username already in use."  } }, null);
        }

        existingUser = await unitOfWork.UserRepository.GetUserByEmailAsync(registerDTO.Email);
        if (existingUser != null)
        {
            return (false, new Dictionary<string, string> { { "Email", "Email already in use." } }, null);
        }

        var user = new AppUser
        {
            UserName = registerDTO.Username,
            Email = registerDTO.Email,
            City = registerDTO.City,
            Country = registerDTO.Country,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await unitOfWork.UserRepository.CreateUser(user, registerDTO.Password);

        if (result.Succeeded)
        {
            await unitOfWork.Complete();
            return (true, null, user);
        }

        return (false, new Dictionary<string, string> {{"", "Server Error"}}, null);
    }

    public async Task<(bool Succeeded, string? ErrorMessage)> LoginAsync(string usernameOrEmail, string password, bool rememberMe)
    {

        var user = await unitOfWork.UserRepository.GetUserByUsernameOrEmailAsync(usernameOrEmail);
        if (user == null)
        {
            return (false, "Invalid login attempt. ");
        }

        bool validPassword = unitOfWork.UserRepository.VerifyPassword(user, password);
        
        if (validPassword)
        {
            await signInManager.SignInAsync(user, rememberMe);
            return (true, null);
        }
        
        return (false, "Invalid login attempt.");
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }
}
