using System;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data;

public class BanRepository(DataContext dataContext) : IBanRepository
{
    public async Task<bool> BanUserAsync(BanDTO banDTO)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => u.Id == banDTO.AppUserId);
        if (user == null) return false;

        var ban = new Ban
        {
            AppUserId = user.Id,
            AppUser = user,
            Reason = banDTO.Reason,
            BannedAt = DateTime.UtcNow,
            BannedUntil = banDTO.BannedUntil,
            IsPermanent = banDTO.IsPermanent,
        };

        user.IsBanned = true;

        dataContext.Bans.Add(ban);
        return true;
    }

    public void CreateBan(Ban ban)
    {
        dataContext.Bans.Add(ban);
    }

    public void DeleteBan(Ban ban)
    {
        dataContext.Bans.Remove(ban);
    }

    public async Task<PagedList<Ban>> GetAllBansAsync(BanFilterParams filterParams)
    {
        var query = dataContext.Bans
            .Include(b => b.AppUser)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filterParams.SearchTerm))
        {
            query = query.Where(b => b.AppUser!.UserName!.Contains(filterParams.SearchTerm));
        }

        if (filterParams.IsPermanenet)
        {
            query = query.Where(b => b.IsPermanent == true);
        }

        if(filterParams.BannedFrom != null)
        {
            query = query.Where(b => b.BannedAt >= filterParams.BannedFrom);
        }

        // order by name
        query = query.OrderBy(b => b.AppUser!.UserName);

        var bans = await query
            .Skip((filterParams.PageNumber - 1) * filterParams.PageSize)
            .Take(filterParams.PageSize)
            .ToListAsync();

        var totalCount = await query.CountAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)filterParams.PageSize);
        return new PagedList<Ban>
        {
            Items = bans,
            TotalCount = totalCount,
            PageSize = filterParams.PageSize,
            CurrentPage = filterParams.PageNumber,
            TotalPages = totalPages
        };

        
    }

    public async Task<Ban?> GetBanByIdAsync(int id)
    {
        return await dataContext.Bans.Where(b => b.Id == id)
            .Include(b => b.AppUser)
            .FirstOrDefaultAsync();
    }

    public async Task<Ban?> GetBanByUserIdAsync(int userId)
    {
        return await dataContext.Bans
            .Include(b => b.AppUser)
            .FirstOrDefaultAsync(b => b.AppUserId == userId);
    }

    public async Task<int> GetBanCountAsync()
    {
        return await dataContext.Bans.CountAsync();
    }

    public async Task<string> GetBanUserNameByIdAsync(int id)
    {
        var userName = await dataContext.Bans
            .Where(b => b.Id == id)
            .Select(b => b.AppUser!.UserName)
            .FirstOrDefaultAsync();

        return userName ?? throw new InvalidOperationException("User name not found.");
    }

    public Task<Ban?> GetUserBanAsync(int userId)
    {
        return dataContext.Bans
            .Include(b => b.AppUser)
            .FirstOrDefaultAsync(b => b.AppUserId == userId);
    }

    public async Task<bool> UnbanUserAsync(int userId)
    {
        var user = await dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return false;

        user.IsBanned = false;
        return true;
    }

    public void UpdateBan(Ban ban)
    {
        dataContext.Entry(ban).State = EntityState.Modified;
    }
}
