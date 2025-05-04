using System;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data;

public class BugRepository(DataContext dataContext) : IBugRepository
{
    public void CreateBug(ReportBug bug)
    {
        if (bug == null)
        {
            throw new ArgumentNullException(nameof(bug), "Bug cannot be null");
        }

        dataContext.ReportBugs.Add(bug);
    }

    public void DeleteBug(ReportBug bug)
    {
        if (bug == null)
        {
            throw new ArgumentNullException(nameof(bug), "Bug cannot be null");
        }

        dataContext.ReportBugs.Remove(bug);
    }

    public async Task DeleteBug(int bugId)
    {
        var bug = await dataContext.ReportBugs.FindAsync(bugId);
        if (bug != null)
        {
            dataContext.ReportBugs.Remove(bug);
        }
        else
        {
            throw new Exception($"Bug with ID {bugId} not found.");
        }
    }

    public async Task<List<ReportBug>?> GetAllBugsAsync()
    {
        return await dataContext.ReportBugs
            .Include(b => b.AppUser)
            .ToListAsync();
    }

    public async Task<ReportBug?> GetBugByIdAsync(int id)
    {
        return await dataContext.ReportBugs
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<int> GetBugCountAsync()
    {
        return await dataContext.ReportBugs.CountAsync();
    }

    public async Task<bool> Solved(int id, bool isResolved)
    {
        var bug = await dataContext.ReportBugs.FindAsync(id);
        if (bug != null)
        {
            bug.IsResolved = isResolved;
            dataContext.Entry(bug).State = EntityState.Modified;
            return true;
        }
        else
        {
            throw new Exception($"Bug with ID {id} not found.");
        }
    }

    public void UpdateBug(ReportBug bug)
    {
        if (bug == null)
        {
            throw new ArgumentNullException(nameof(bug), "Bug cannot be null");
        }

        dataContext.Entry(bug).State = EntityState.Modified;
    }
}
