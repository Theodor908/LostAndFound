using System;
using LostAndFound.Entities;
using LostAndFound.Helpers;

namespace LostAndFound.Interfaces;

public interface IReportRepository
{
    void ReportPost(ReportPost reportPost);
    void ReportBug(ReportBug reportBug);
    void ReportUser(ReportUser reportUser);
    void DeleteReportPost(int reportPostId);
    void DeleteReportBug(int reportBugId);
    void DeleteReportUser(int reportUserId);
    Task<bool> ResolveReportPostAsync(int reportPostId);
    Task<bool> ResolveReportBugAsync(int reportBugId);  
    Task<bool> ResolveReportUserAsync(int reportUserId);
    Task<PagedList<ReportPost>> GetAllReportPostsAsync(int pageNumber, int pageSize = 10, string? search = null);
    Task<PagedList<ReportBug>> GetAllReportBugsAsync(int pageNumber, int pageSize = 10, string? search = null);
    Task<PagedList<ReportUser>> GetAllReportUsersAsync(int pageNumber, int pageSize = 10, string? search = null);
}

