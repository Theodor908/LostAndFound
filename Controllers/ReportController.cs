using System;
using System.Threading.Tasks;
using LostAndFound.Extensions;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LostAndFound.Controllers;
public class ReportController(IUserService userService, IReportService reportService) : Controller
{
    public async Task<IActionResult> ReportPost(int id)
    {
        var reportPostDTO = new ReportPostDTO
        {
            PostId = id,
            ReportedByUserId = User.GetUserId(),
            ReportedByUser = await userService.GetMemberByIdAsync(User.GetUserId()),
            ReportedUserId = id,
            ReportedUser = await userService.GetMemberByIdAsync(id),
            Title = "",
            Description = ""
        };
        return View(reportPostDTO);
    }

    public IActionResult ReportBug()
    {
        var reportBugDTO = new ReportBugDTO
        {
            Title = "",
            Description = ""
        };
        return View(reportBugDTO);
    }

    public async Task<IActionResult> ReportUser(int id)
    {
        var reportUserDTO = new ReportUserDTO
        {
            Id = id,
            ReportedByUserId = User.GetUserId(),
            ReportedByUser = await userService.GetUserByIdAsync(User.GetUserId()),
            ReportedUserId = id,
            ReportedUser = await userService.GetUserByIdAsync(id),
            Title = "",
            Description = ""
        };
        return View(reportUserDTO);
    }

    public async Task<IActionResult> ReportPostDetails(int id)
    {
        var post = await reportService.GetReportPostByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return View(post);
    }

    public async Task<IActionResult> ReportUserDetails(int id)
    {
        var user = await reportService.GetReportUserByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    public async Task<IActionResult> ReportBugDetails(int id)
    {
        var bug = await reportService.GetReportBugByIdAsync(id);
        if (bug == null)
        {
            return NotFound();
        }
        return View(bug);
    }

    [HttpPost]
    public async Task<IActionResult> ReportPost(ReportPostDTO reportPostDTO)
    {
        if(ModelState.IsValid == false)
        {
            return View(reportPostDTO);
        }
        var result = await reportService.ReportPostAsync(reportPostDTO);
        if(result == false)
        {
            return StatusCode(500, "Internal server error");
        }
        return RedirectToAction("ReportPostDetails", new { id = reportPostDTO.PostId });
    }

    [HttpPost]
    public async Task<IActionResult> ReportBug(ReportBugDTO reportBugDTO)
    {
        if(ModelState.IsValid == false)
        {
            return View(reportBugDTO);
        }
        var result = await reportService.ReportBugAsync(reportBugDTO);
        if(result == false)
        {
            return StatusCode(500, "Internal server error");
        }
        return RedirectToAction("ReportBugDetails", new { id = reportBugDTO.Id });
    }

    [HttpPost]
    public async Task<IActionResult> ReportUser(ReportUserDTO reportUserDTO)
    {
        if(ModelState.IsValid == false)
        {
            return View(reportUserDTO);
        }
        var result = await reportService.ReportUserAsync(reportUserDTO);
        if(result == false)
        {
            return StatusCode(500, "Internal server error");
        }
        return RedirectToAction("ReportUserDetails", new { id = reportUserDTO.Id });
    }

    public async Task<IActionResult> SolvePost(int id)
    {
        var result = await reportService.ResolveReportPostAsync(id);
        if(result == false)
        {
            return StatusCode(500, "Internal server error");
        }
        return RedirectToAction("ReportPostDetails", new { id = id });
    }

    public async Task<IActionResult> SolveBug(int id)
    {
        var result = await reportService.ResolveReportBugAsync(id);
        if(result == false)
        {
            return StatusCode(500, "Internal server error");
        }
        return RedirectToAction("ReportBugDetails", new { id = id });
    }

    public async Task<IActionResult> SolveUser(int id)
    {
        var result = await reportService.ResolveReportUserAsync(id);
        if(result == false)
        {
            return StatusCode(500, "Internal server error");
        }
        return RedirectToAction("ReportUserDetails", new { id = id });
    }

    public async Task<IActionResult> DeleteReportPost(int id)
    {
        var result = await reportService.DeleteReportPostAsync(id);
        if(result == false)
        {
            return StatusCode(500, "Internal server error");
        }
        return RedirectToAction("ReportPostList", "Admin");
    }

    public async Task<IActionResult> DeleteReportBug(int id)
    {
        var result = await reportService.DeleteReportBugAsync(id);
        if(result == false)
        {
            return StatusCode(500, "Internal server error");
        }
        return RedirectToAction("ReportBugList", "Admin");
    }

    public async Task<IActionResult> DeleteReportUser(int id)
    {
        var result = await reportService.DeleteReportUserAsync(id);
        if(result == false)
        {
            return StatusCode(500, "Internal server error");
        }
        return RedirectToAction("ReportUserList", "Admin");
    }
}
