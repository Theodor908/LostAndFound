using System;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;
[Authorize]
public class ReportController(IUserService userService) : Controller
{
    public IActionResult ReportPost(int postId)
    {
        return View();
    }

    public IActionResult ReportBug()
    {
        return View();
    }

    public IActionResult ReportUser(int userId)
    {
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ReportPost(ReportPostDTO reportPostDTO)
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> ReportBug(ReportBugDTO reportBugDTO)
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> ReportUser(ReportUserDTO reportUserDTO)
    {
        return null;
    }

}
