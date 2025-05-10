using System;
using LostAndFound.Entities;
using LostAndFound.Helpers;
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
    Task<bool> DeleteReportPostAsync(int reportPostId);
    Task<bool> DeleteReportBugAsync(int reportBugId);
    Task<bool> DeleteReportUserAsync(int reportUserId);

    Task<int> GetReportPostCountAsync();
    Task<int> GetReportBugCountAsync();
    Task<int> GetReportUserCountAsync();

    Task<ReportPostDTO> GetReportPostByIdAsync(int reportPostId);
    Task<ReportBugDTO> GetReportBugByIdAsync(int reportBugId);
    Task<ReportUserDTO> GetReportUserByIdAsync(int reportUserId);


    Task<ReportBugsListDTO> GetAllReportBugsAsync(ReportBugFilterParams reportBugFilterParams); 
    Task<ReportPostsListDTO> GetAllReportPostsAsync(ReportPostFilterParams reportBugFilterParams);
    Task<ReportUsersListDTO> GetAllReportUsersAsync(ReportUserFilterParams reportBugFilterParams);
}
