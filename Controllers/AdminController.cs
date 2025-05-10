using System;
using System.Threading.Tasks;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using LostAndFound.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;

public class AdminController(IAdminService adminService, IUserService userService, IRoleService roleService, IPostService postService, ICategoryService categoryService, IReportService reportService, IBanService banService) : Controller
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

    public async Task<IActionResult> UserList(bool isBanned = false, string? searchTerm = null, string? sortBy = null, int pageNumber = 1, int pageSize = 10)
    {
        var userFilterParams = new UserFilterParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            SortBy = sortBy,
            IsBanned = isBanned

        };
    
        var users = await userService.GetAllUsersAsync(userFilterParams);
        users.SortBy = sortBy;
        if (users == null)
        {
            return NotFound("No users found.");
        }
        return View(users);
    }

    public async Task<IActionResult> RoleList(string? searchTerm = null, string? sortBy = null, int pageNumber = 1, int pageSize = 10)
    {
        var roleFilterParams = new RoleFilterParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            SortBy = sortBy
        };
        var roles = await roleService.GetAllRolesAsync(roleFilterParams);
        if (roles == null)
        {
            return NotFound("No roles found.");
        }
        return View(roles);
    }

    public async Task<IActionResult> PostList(string? searchTerm = null, string? postType = "", bool? isActive = null, string? sortBy = null, int pageNumber = 1, int pageSize = 10)
    {
        var postFilterParams = new PostFilterParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            SortBy = sortBy,
            PostType = postType,
            IsActive = isActive
        };
        var posts = await postService.GetAllPostsAsync(postFilterParams);
        if (posts == null)
        {
            return NotFound("No posts found.");
        }
        return View(posts);
    }

    public async Task<IActionResult> CategoryList(string? searchTerm = null, int pageNumber = 1, int pageSize = 10)
    {
        var categoryFilterParams = new CategoryFilterParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm,
        };
        var categories = await categoryService.GetAllCategoriesAsync(categoryFilterParams);
        if (categories == null)
        {
            return NotFound("No categories found.");
        }
        return View(categories);
    }

    public async Task<IActionResult> ReportBugList(string? searchTerm = null, bool isResolved = false, string? sortBy = null, int pageNumber = 1, int pageSize = 10)
    {
        var reportBugFilterParams = new ReportBugFilterParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            SortBy = sortBy,
            IsResolved = isResolved
        };
        var reportBugs = await reportService.GetAllReportBugsAsync(reportBugFilterParams);
        if (reportBugs == null)
        {
            return NotFound("No report bugs found.");
        }
        return View(reportBugs);
    }

    public async Task<IActionResult> ReportPostList(string? searchTerm = null, bool isResolved = false, string? sortBy = null, int pageNumber = 1, int pageSize = 10)
    {
        var reportPostFilterParams = new ReportPostFilterParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            SortBy = sortBy,
            IsResolved = isResolved
        };
        var reportPosts = await reportService.GetAllReportPostsAsync(reportPostFilterParams);
        if (reportPosts == null)
        {
            return NotFound("No report posts found.");
        }
        return View(reportPosts);
    }

    public async Task<IActionResult> ReportUserList(string? searchTerm = null, bool isResolved = false, string? sortBy = null, int pageNumber = 1, int pageSize = 10)
    {
        var reportUserFilterParams = new ReportUserFilterParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            SortBy = sortBy,
            IsResolved = isResolved
        };
        var reportUsers = await reportService.GetAllReportUsersAsync(reportUserFilterParams);
        if (reportUsers == null)
        {
            return NotFound("No report users found.");
        }
        return View(reportUsers);
    }

    public async Task<IActionResult> BansList(string? searchTerm = null, bool isPermanent = false, string? sortBy = null, int pageNumber = 1, int pageSize = 10)
    {
        var reportBanFilterParams = new BanFilterParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            SearchTerm = searchTerm,
            IsPermanenet = isPermanent
        };
        var reportUsers = await banService.GetBannedUsersAsync(reportBanFilterParams);
        if (reportUsers == null)
        {
            return NotFound("No report users found.");
        }
        return View(reportUsers);
    }

    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult UserDetails(int id)
    {
        return RedirectToAction("Profile", "User", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<IActionResult> BanDetails(int id)
    {
        var banDTO = await banService.GetBanByIdAsync(id);
        if (banDTO == null)
        {
            return NotFound("Ban data not found.");
        }
        return View(banDTO);
    }
    [Authorize(Roles = "Admin")]
    public IActionResult RoleDetails(int id)
    {
        return RedirectToAction("RoleDetails", "Role", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult PostDetails(int id)
    {
        return RedirectToAction("PostDetails", "Post", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult CategoryDetails(int id)
    {
        return RedirectToAction("CategoryDetails", "Category", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult ReportBugDetails(int id)
    {
        return RedirectToAction("ReportBugDetails", "Report", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult ReportPostDetails(int id)
    {
        return RedirectToAction("ReportPostDetails", "Report", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult ReportUserDetails(int id)
    {
        return RedirectToAction("ReportUserDetails", "Report", new { id });
    }
    // Edit actions
    [Authorize(Roles = "Admin")]
    public IActionResult UserDelete(int id)
    {
        return RedirectToAction("DeleteUser", "User", new { id , actionByAdmin = true});
    }
    [Authorize(Roles = "Admin")]
    public IActionResult RoleDelete(int id)
    {
        return RedirectToAction("DeleteRole", "Role", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult PostDelete(int id)
    {
        return RedirectToAction("PostDelete", "Post", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult ReportBugDelete(int id)
    {
        return RedirectToAction("DeleteReportBug", "Report", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult ReportPostDelete(int id)
    {
        return RedirectToAction("DeleteReportPost", "Report", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult ReportUserDelete(int id)
    {
        return RedirectToAction("DeleteReportUser", "Report", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult CategoryCreate(int id)
    {
        return RedirectToAction("CreateCategory", "Category", new { id });
    }
    [Authorize(Roles = "Admin, Moderator")]
    public IActionResult CategoryDelete(int id)
    {
        return RedirectToAction("DeleteCategory", "Category", new { id });
    }
    // Edit actions
    [Authorize(Roles = "Admin")]
    public IActionResult RoleCreate()
    {
        return RedirectToAction("CreateRole", "Role");
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> BanUser(int id)
    {
        var banDTO = new BanDTO
        {
            AppUserId = id,
            AppUser = await userService.GetUserByIdAsync(id),
            Reason = string.Empty,
            BannedAt = DateTime.UtcNow,
            BannedUntil = null,
            IsPermanent = false
        };
        if (banDTO.AppUser == null)
        {
            return NotFound("User not found.");
        }
        return View(banDTO);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> BanUser(BanDTO banDTO)
    {
        var result = await banService.BanUserAsync(banDTO);
        if (!result)
        {
            return StatusCode(500, "Internal server error");
        }
        return RedirectToAction("UserList", new { isBanned = true });
    }

    public async Task<IActionResult> UnbanUser(int id)
    {
        await banService.UnbanUserAsync(id);
        return RedirectToAction("UserList", new { isBanned = false });
    }

    public async Task<IActionResult> BanDelete(int id)
    {
        var banDTO = await banService.DeleteBanByIdAsync(id);
        if (banDTO == null)
        {
            return NotFound("Ban data not found.");
        }
        return RedirectToAction("BansList");
    }

    public IActionResult UserRoleAssignment(int id)
    {
        return RedirectToAction("AssignRoles", "Role", new { id });
    }

}
