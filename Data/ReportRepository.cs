using System;

namespace LostAndFound.Data;

using System.Collections.Generic;
using System.Threading.Tasks;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.EntityFrameworkCore;

public class ReportRepository (DataContext dataContext) : IReportRepository
{
    public void DeleteReportPost(int reportPostId)
    {
        var reportPost = dataContext.ReportPosts.Find(reportPostId);
        if (reportPost != null)
        {
            dataContext.ReportPosts.Remove(reportPost);
        }
    }

    public void DeleteReportBug(int reportBugId)
    {
        var reportBug = dataContext.ReportBugs.Find(reportBugId);
        if (reportBug != null)
        {
            dataContext.ReportBugs.Remove(reportBug);
        }
    }

    public void DeleteReportUser(int reportUserId)
    {
        var reportUser = dataContext.ReportUsers.Find(reportUserId);
        if (reportUser != null)
        {
            dataContext.ReportUsers.Remove(reportUser);
        }
    }

    public void ReportBug(ReportBug reportBug)
    {
        dataContext.ReportBugs.Add(reportBug);
    }

    public void ReportPost(ReportPost reportPost)
    {
        dataContext.ReportPosts.Add(reportPost);
    }

    public void ReportUser(ReportUser reportUser)
    {
        dataContext.ReportUsers.Add(reportUser);
    }

    public async Task<int> GetReportPostCountAsync()
    {
        return await dataContext.ReportPosts.CountAsync();
    }

    public async Task<int> GetReportBugCountAsync()
    {
        return await dataContext.ReportBugs.CountAsync();
    }

    public async Task<int> GetReportUserCountAsync()
    {
        return await dataContext.ReportUsers.CountAsync();
    }

    public async Task<bool> ResolveReportPostAsync(int reportPostId)
    {
        var reportPost = await dataContext.ReportPosts.FindAsync(reportPostId);
        if (reportPost != null)
        {
            reportPost.IsResolved = true;
            dataContext.ReportPosts.Update(reportPost);
            return true;
        }
        return false;
    }

    public async Task<bool> ResolveReportBugAsync(int reportBugId)
    {
        var reportBug = await dataContext.ReportBugs.FindAsync(reportBugId);
        if (reportBug != null)
        {
            reportBug.IsResolved = true;
            dataContext.ReportBugs.Update(reportBug);
            return true;
        }
        return false;
    }

    public async Task<bool> ResolveReportUserAsync(int reportUserId)
    {
        var reportUser = await dataContext.ReportUsers.FindAsync(reportUserId);
        if (reportUser != null)
        {
            reportUser.IsResolved = true;
            dataContext.ReportUsers.Update(reportUser);
            return true;
        }
        return false;
    }

    public async Task<PagedList<ReportPost>> GetReportedPostsAsync(ReportPostFilterParams filterParams)
    {
        var query = dataContext.ReportPosts
            .Include(rp => rp.Post)
                .ThenInclude(p => p!.AppUser)
            .AsQueryable();

        if (filterParams.IsResolved)
        {
            query = query.Where(rp => rp.IsResolved == filterParams.IsResolved);
        }

        if (!string.IsNullOrEmpty(filterParams.SearchTerm))
        {
            var searchTerm = filterParams.SearchTerm.ToLower();
            query = query.Where(rp =>
                rp.Post!.Title.ToLower().Contains(searchTerm) ||
                rp.Post!.Description.ToLower().Contains(searchTerm));
        }

        var totalCount = await query.CountAsync();

        if (!string.IsNullOrEmpty(filterParams.SortBy))
        {
            query = filterParams.SortBy.ToLower() switch
            {
                "date" => filterParams.SortDescending
                    ? query.OrderByDescending(rp => rp.CreatedAt)
                    : query.OrderBy(rp => rp.CreatedAt),
                "title" => filterParams.SortDescending
                    ? query.OrderByDescending(rp => rp.Post!.Title)
                    : query.OrderBy(rp => rp.Post!.Title),
                _ => filterParams.SortDescending
                    ? query.OrderByDescending(rp => rp.CreatedAt)
                    : query.OrderBy(rp => rp.CreatedAt)
            };
        }
        else
        {
            query = query.OrderByDescending(rp => rp.CreatedAt);
        }

        var reportedPosts = await query
            .Skip((filterParams.PageNumber - 1) * filterParams.PageSize)
            .Take(filterParams.PageSize)
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)filterParams.PageSize);
        return new PagedList<ReportPost>
        {
            Items = reportedPosts,
            TotalCount = totalCount,
            PageSize = filterParams.PageSize,
            CurrentPage = filterParams.PageNumber,
            TotalPages = totalPages
        };
    }

    public async Task<PagedList<ReportBug>> GetReportedBugsAsync(ReportBugFilterParams filterParams)
    {
        var query = dataContext.ReportBugs
            .AsQueryable();

        if (filterParams.IsResolved)
        {
            query = query.Where(rb => rb.IsResolved == filterParams.IsResolved);
        }

        if (!string.IsNullOrEmpty(filterParams.SearchTerm))
        {
            var searchTerm = filterParams.SearchTerm.ToLower();
            query = query.Where(rb =>
                rb.Title.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase) ||
                rb.Description.Contains(searchTerm, StringComparison.CurrentCultureIgnoreCase));
        }

        var totalCount = await query.CountAsync();

        if (!string.IsNullOrEmpty(filterParams.SortBy))
        {
            query = filterParams.SortBy.ToLower() switch
            {
                "date" => filterParams.SortDescending
                    ? query.OrderByDescending(rb => rb.CreatedAt)
                    : query.OrderBy(rb => rb.CreatedAt),
                "title" => filterParams.SortDescending
                    ? query.OrderByDescending(rb => rb.Title)
                    : query.OrderBy(rb => rb.Title),
                _ => filterParams.SortDescending
                    ? query.OrderByDescending(rb => rb.CreatedAt)
                    : query.OrderBy(rb => rb.CreatedAt)
            };
        }
        else
        {
            query = query.OrderByDescending(rb => rb.CreatedAt);
        }

        var reportedBugs = await query
            .Skip((filterParams.PageNumber - 1) * filterParams.PageSize)
            .Take(filterParams.PageSize)
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)filterParams.PageSize);
        return new PagedList<ReportBug>
        {
            Items = reportedBugs,
            TotalCount = totalCount,
            PageSize = filterParams.PageSize,
            CurrentPage = filterParams.PageNumber,
            TotalPages = totalPages
        };
    }

    public async Task<PagedList<ReportUser>> GetReportedUsersAsync(ReportUserFilterParams filterParams)
    {
        var query = dataContext.ReportUsers
            .Include(ru => ru.ReportedByUser)
            .AsQueryable();

        if (filterParams.IsResolved)
        {
            query = query.Where(ru => ru.IsResolved == filterParams.IsResolved);
        }

        if (!string.IsNullOrEmpty(filterParams.SearchTerm))
        {
            var searchTerm = filterParams.SearchTerm.ToLower();
            query = query.Where(ru =>
                ru.ReportedByUser!.UserName!.ToLower().Contains(searchTerm));
        }

        var totalCount = await query.CountAsync();

        if (!string.IsNullOrEmpty(filterParams.SortBy))
        {
            query = filterParams.SortBy.ToLower() switch
            {
                "date" => filterParams.SortDescending
                    ? query.OrderByDescending(ru => ru.CreatedAt)
                    : query.OrderBy(ru => ru.CreatedAt),
                "name" => filterParams.SortDescending
                    ? query.OrderByDescending(ru => ru.ReportedByUser!.UserName!)
                    : query.OrderBy(ru => ru.ReportedByUser!.UserName!),
                _ => filterParams.SortDescending
                    ? query.OrderByDescending(ru => ru.CreatedAt)
                    : query.OrderBy(ru => ru.CreatedAt)
            };
        }
        else
        {
            query = query.OrderByDescending(ru => ru.CreatedAt);
        }

        var reportedUsers = await query
            .Skip((filterParams.PageNumber - 1) * filterParams.PageSize)
            .Take(filterParams.PageSize)
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)filterParams.PageSize);
        return new PagedList<ReportUser>
        {
            Items = reportedUsers,
            TotalCount = totalCount,
            PageSize = filterParams.PageSize,
            CurrentPage = filterParams.PageNumber,
            TotalPages = totalPages
        };
    }

    public Task<ReportPost> GetReportPostByIdAsync(int reportPostId)
    {
        var post = dataContext.ReportPosts.FirstOrDefaultAsync(rp => rp.Id == reportPostId);
        if (post == null) return null!;
        return post!;
    }

    public Task<ReportBug> GetReportBugByIdAsync(int reportBugId)
    {
        var bug = dataContext.ReportBugs.FirstOrDefaultAsync(rb => rb.Id == reportBugId);
        if (bug == null) return null!;
        return bug!;
    }

    public Task<ReportUser> GetReportUserByIdAsync(int reportUserId)
    {
        var user = dataContext.ReportUsers.FirstOrDefaultAsync(ru => ru.Id == reportUserId);
        if (user == null) return null!;
        return user!;
    }
}
