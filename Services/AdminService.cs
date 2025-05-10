using System;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class AdminService(IRoleService roleService, ICategoryService categoryService, IUserService userService, IPostService postService, IItemService itemService, IBanService banService, IReportService reportService) : IAdminService
{

    public Task<bool> BanUserAsync(int userId, string reason, DateTime? banEndDate = null, bool deleteAccount = false)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UnbanUserAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddRoleToUserAsync(int userId, string roleName)
    {
        var result = await roleService.AddRoleToUserAsync(userId, roleName);
        if (!result)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> RemoveRoleFromUserAsync(int userId, string roleName)
    {
        return await roleService.RemoveRoleFromUserAsync(userId, roleName);
    }

    public async Task<AdminDTO?> GetAdminDashboardDataAsync()
    {
        int countUsers = await userService.GetUserCountAsync();
        int countPosts = await postService.GetPostCountAsync();
        int countCategories = await categoryService.GetCategoriesCountAsync();
        int countRoles = await roleService.GetRoleCountAsync();
        int countItems = await itemService.GetItemCountAsync();
        int countBans = await banService.GetBanCountAsync();
        int countBugReports = await reportService.GetReportBugCountAsync();
        int countPostReports = await reportService.GetReportPostCountAsync();
        int countUserReports = await reportService.GetReportUserCountAsync();


        var adminDashboardData = new AdminDTO
        {
            CountUsers = countUsers,
            CountPosts = countPosts,
            CountCategories = countCategories,
            CountRoles = countRoles,
            CountItems = countItems,
            CountBans = countBans,
            CountBugReports = countBugReports,
            CountPostReports = countPostReports,
            CountUserReports = countUserReports
        };

        return adminDashboardData;
    }
}
