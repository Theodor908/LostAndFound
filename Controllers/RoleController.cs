using System;
using System.Threading.Tasks;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using LostAndFound.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;

[Authorize(Roles = "Admin")]
public class RoleController(IRoleService roleService) : Controller
{

    public async Task<IActionResult> RoleDetails(int id)
    {       
        var role = await roleService.GetRoleByIdAsync(id);
        if (role == null)
        {
            return NotFound("Role not found.");
        }
        var users = await roleService.GetUsersInRoleByIdAsync(id);
        int userCount = 0;
        if (users != null)
        {
            userCount = users.Count;
        }
        var roleDetailsDTO = new RoleDetailsDTO
        {
            Id = role.Id,
            RoleName = role.Name,
            UsersInRole = userCount
        };
        return View(roleDetailsDTO);
    }

    public IActionResult CreateRole()
    {
        return View(new RoleDTO());
    }

    public async Task<IActionResult> EditRole(int id)
    {
        var role = await roleService.GetRoleByIdAsync(id);
        if (role == null)
        {
            return NotFound("Role not found.");
        }
        var roleDTO = new RoleDTO
        {
            Id = role.Id,
            Name = role.Name
        };
        return View(roleDTO);
    }
    public async Task<IActionResult> DeleteRole(int id)
    {
        var role = await roleService.GetRoleByIdAsync(id);
        if (role == null)
        {
            return NotFound("Role not found.");
        }
        var roleDeleted = await roleService.DeleteRoleAsync(id);
        if (!roleDeleted)
        {
            return BadRequest("Unable to delete role.");
        }
        return RedirectToAction("RoleList", "Admin");
    }
    [HttpPost]
    public async Task<IActionResult> CreateRole(RoleDTO roleDTO)
    {
        if (!ModelState.IsValid)
        {
            return View(roleDTO);
        }

        var roleCreated = await roleService.CreateRoleAsync(roleDTO.Name);
        if (!roleCreated)
        {
            return BadRequest("Unable to create role.");
        }
        var role = await roleService.GetRoleByNameAsync(roleDTO.Name);
        if (role == null)
        {
            return NotFound("Role not found.");
        }

        return RedirectToAction("RoleDetails", new { id = role.Id });
    }
    [HttpPost]
    public async Task<IActionResult> EditRole(RoleDTO roleDTO)
    {
        if (!ModelState.IsValid)
        {
            return View(roleDTO);
        }

        var role = await roleService.UpdateRoleAsync(roleDTO.Id, roleDTO.Name);
        if (!role)
        {
            return NotFound("Unable to update role.");
        }

        return RedirectToAction("RoleDetails", new { id = roleDTO.Id });
    }

    public async Task<IActionResult> AssignRoles(int id)
    {
        var userRoleAssignmentDTO = await roleService.GetUserRoleAssignmentByIdAsync(id);
        var assignRolesDTO = new UserRoleAssignmentDTO
        {
            UserId = id,
            RoleAssignments = userRoleAssignmentDTO?.Select(ra => new RoleAssignmentDTO
            {
                Role = ra.Role,
                IsAssigned = ra.IsAssigned
            }).ToList()!
        };
        return View(assignRolesDTO);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRoles(UserRoleAssignmentDTO userRoleAssignmentDTO)
    {
        await roleService.UpdateUserRoleAssignmentAsync(userRoleAssignmentDTO.UserId, userRoleAssignmentDTO.RoleAssignments);

        return RedirectToAction("AssignRoles", new { id = userRoleAssignmentDTO.UserId });
    }

}
