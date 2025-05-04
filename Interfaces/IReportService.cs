using System;
using LostAndFound.Entities;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IReportService 
{

    Task<bool> ReportPostAsync(ReportPostDTO reportPostDTO);
    Task<bool> ReportBugAsync(ReportBugDTO reportBugDTO);
    Task<bool> ReportUserAsync(ReportUserDTO reportUserDTO);
    Task<bool> ResolveReportPostAsync(int reportPostId);
    Task<bool> ResolveReportBugAsync(int reportBugId);  
    Task<bool> ResolveReportUserAsync(int reportUserId);
    Task<List<ReportPostDTO>?> GetAllReportPostsAsync();
    Task<List<ReportBugDTO>?> GetAllReportBugsAsync();
    Task<List<ReportUserDTO>?> GetAllReportUsersAsync();

}
