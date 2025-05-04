using System;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data;

public class BanRepository(DataContext dataContext) : IBanRepository
{
    public void CreateBan(Ban ban)
    {
        dataContext.Bans.Add(ban);
    }

    public void DeleteBan(Ban ban)
    {
        dataContext.Bans.Remove(ban);
    }

    public async Task<List<Ban>?> GetAllBansAsync()
    {
        return await dataContext.Bans.ToListAsync();
    }

    public async Task<Ban?> GetBanByIdAsync(int id)
    {
        return await dataContext.Bans
            .Include(b => b.AppUser)
            .FirstOrDefaultAsync(b => b.Id == id);
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

    public void UpdateBan(Ban ban)
    {
        dataContext.Entry(ban).State = EntityState.Modified;
    }
}
