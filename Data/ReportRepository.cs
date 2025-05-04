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

    public async Task<bool> ResolveReportPostAsync(int reportPostId)
    {
        var reportPost = await dataContext.ReportPosts.FindAsync(reportPostId);
        if (reportPost != null)
        {
            reportPost.IsResolved = true;
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
            return true;
        }
        return false;
    }

    public Task<PagedList<ReportPost>> GetAllReportPostsAsync(int pageNumber, int pageSize = 10, string? search = null)
    {
        PaginationHelper<ReportPost> paginationHelper = new(dataContext);
        var query = dataContext.ReportPosts.AsQueryable();
        var searchFunction = new Func<IQueryable<ReportPost>, string, IQueryable<ReportPost>>((q, s) =>
        {
            return q.Where(r => r.Description.Contains(s) || r.Description.Contains(s));
        });
        return paginationHelper.GetPagedItemsAsync(searchFunction, pageNumber, pageSize, search);
    }

    public Task<PagedList<ReportBug>> GetAllReportBugsAsync(int pageNumber, int pageSize = 10, string? search = null)
    {
        PaginationHelper<ReportBug> paginationHelper = new(dataContext);
        var query = dataContext.ReportBugs.AsQueryable();
        var searchFunction = new Func<IQueryable<ReportBug>, string, IQueryable<ReportBug>>((q, s) =>
        {
            return q.Where(r => r.Description.Contains(s) || r.Description.Contains(s));
        });
        return paginationHelper.GetPagedItemsAsync(searchFunction, pageNumber, pageSize, search);
    }

    public Task<PagedList<ReportUser>> GetAllReportUsersAsync(int pageNumber, int pageSize = 10, string? search = null)
    {
        PaginationHelper<ReportUser> paginationHelper = new(dataContext);
        var query = dataContext.ReportUsers.AsQueryable();
        var searchFunction = new Func<IQueryable<ReportUser>, string, IQueryable<ReportUser>>((q, s) =>
        {
            return q.Where(r => r.Description.Contains(s) || r.Description.Contains(s));
        });
        return paginationHelper.GetPagedItemsAsync(searchFunction, pageNumber, pageSize, search);
    }
}
