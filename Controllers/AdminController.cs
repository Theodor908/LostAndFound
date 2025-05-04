using System;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using LostAndFound.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;

public class AdminController(IAdminService adminService, IUserService userService, IRoleService roleService) : Controller
{
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        AdminDTO? adminDTO = await adminService.GetAdminDashboardDataAsync();
        if (adminDTO == null)
        {
            return NotFound("Admin data not found.");
        }
        return View(adminDTO);
    }
    [HttpGet]
    public async Task<IActionResult> UserList(UserFilterParams userFilterParams)
    {
        var users = await userService.GetAllUsersAsync(userFilterParams);
        if (users == null)
        {
            return NotFound("No users found.");
        }
        return View(users);
    }

    [HttpGet]
    public async Task<IActionResult> RoleList(int pageNumber = 1, string? searchTerm = null)
    {
        var roles = await roleService.GetAllRolesAsync(pageNumber, 10, searchTerm);
        if (roles == null)
        {
            return NotFound("No roles found.");
        }
        return View(roles);
    }

    public async Task<IActionResult> UserView(int id)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        return RedirectToAction("Details", "User", new { id });
    }

}
