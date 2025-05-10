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
    Task<int> GetReportPostCountAsync();
    Task<int> GetReportBugCountAsync();
    Task<int> GetReportUserCountAsync();

    Task<ReportPost> GetReportPostByIdAsync(int reportPostId);
    Task<ReportBug> GetReportBugByIdAsync(int reportBugId);
    Task<ReportUser> GetReportUserByIdAsync(int reportUserId);
    Task<PagedList<ReportPost>> GetReportedPostsAsync(ReportPostFilterParams filterParams);
    Task<PagedList<ReportBug>> GetReportedBugsAsync(ReportBugFilterParams filterParams);
    Task<PagedList<ReportUser>> GetReportedUsersAsync(ReportUserFilterParams filterParams);
}

