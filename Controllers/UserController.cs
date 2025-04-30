using System;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;

[Authorize]
public class UserController(IUserService userService) : Controller
{

        public async Task<IActionResult> Profile(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = user;
            return View(model);
        }
    [HttpGet]
    public async Task<IActionResult> EditProfile(int id)
    {
        var user = await userService.GetMemberByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var model = user;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> EditProfile(MemberDTO memberDTO)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("ModelState is not valid");
            return View(memberDTO);
        }

        var result = await userService.UpdateMemberAsync(memberDTO);
        if (!result)
        {
            return StatusCode(500, "Internal server error");
        }

        return RedirectToAction("Profile", new { id = memberDTO.Id });
    }

    [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await userService.DeleteUserAsync(id);
            if (!result)
            {
                return StatusCode(500, "Internal server error");
            }

            return RedirectToAction("Index", "Home");
        }

}
