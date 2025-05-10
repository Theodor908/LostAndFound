using System;
using System.Threading.Tasks;
using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class ReportService(IUnitOfWork unitOfWork, IMapper mapper) : IReportService
{
    public async Task<bool> DeleteReportBugAsync(int reportBugId)
    {
        unitOfWork.ReportRepository.DeleteReportBug(reportBugId);
        return await Done();
    }

    public async Task<bool> DeleteReportPostAsync(int reportPostId)
    {
        unitOfWork.ReportRepository.DeleteReportPost(reportPostId);
        return await Done();
    }

    public async Task<bool> DeleteReportUserAsync(int reportUserId)
    {
        unitOfWork.ReportRepository.DeleteReportUser(reportUserId);
        return await Done();
    }

    public async Task<ReportBugsListDTO> GetAllReportBugsAsync(ReportBugFilterParams reportBugFilterParams)
    {
        var reportBugs = await unitOfWork.ReportRepository.GetReportedBugsAsync(reportBugFilterParams);
        if (reportBugs == null) return null!;
        var reportBugsDTO = mapper.Map<PagedList<ReportBugDTO>>(reportBugs);
        ReportBugsListDTO reportBugsListDTO = new()
        {
            SearchTerm = reportBugFilterParams.SearchTerm,
            IsResolved = reportBugFilterParams.IsResolved,
            ReportBugsList = reportBugsDTO,
        };
        return reportBugsListDTO;
    }

    public async Task<ReportPostsListDTO> GetAllReportPostsAsync(ReportPostFilterParams reportPostFilterParams)
    {
        var reportPosts = await unitOfWork.ReportRepository.GetReportedPostsAsync(reportPostFilterParams);
        if (reportPosts == null) return null!;
        var reportPostsDTO = mapper.Map<PagedList<ReportPostDTO>>(reportPosts);
        ReportPostsListDTO reportPostsListDTO = new()
        {
            SearchTerm = reportPostFilterParams.SearchTerm,
            IsResolved = reportPostFilterParams.IsResolved,
            ReportPostsList = reportPostsDTO,
        };
        return reportPostsListDTO;
    }

    public async Task<ReportUsersListDTO> GetAllReportUsersAsync(ReportUserFilterParams reportUserFilterParams)
    {
        var reportUsers = await unitOfWork.ReportRepository.GetReportedUsersAsync(reportUserFilterParams);
        if (reportUsers == null) return null!;
        var reportUsersDTO = mapper.Map<PagedList<ReportUserDTO>>(reportUsers);
        ReportUsersListDTO reportUsersListDTO = new()
        {
            SearchTerm = reportUserFilterParams.SearchTerm,
            IsResolved = reportUserFilterParams.IsResolved,
            ReportUsersList = reportUsersDTO,
        };
        return reportUsersListDTO;
    }

    public async Task<ReportBugDTO> GetReportBugByIdAsync(int reportBugId)
    {
        var reportBug = await unitOfWork.ReportRepository.GetReportBugByIdAsync(reportBugId);
        if (reportBug == null) return null!;
        var reportBugDTO = mapper.Map<ReportBugDTO>(reportBug);
        return reportBugDTO;
    }

    public async Task<int> GetReportBugCountAsync()
    {
        return await unitOfWork.ReportRepository.GetReportBugCountAsync();
    }

    public async Task<ReportPostDTO> GetReportPostByIdAsync(int reportPostId)
    {
        var reportPost = await unitOfWork.ReportRepository.GetReportPostByIdAsync(reportPostId);
        if (reportPost == null) return null!;
        var reportPostDTO = mapper.Map<ReportPostDTO>(reportPost);
        return reportPostDTO;
    }

    public async Task<int> GetReportPostCountAsync()
    {
        return await unitOfWork.ReportRepository.GetReportPostCountAsync();
    }

    public async Task<ReportUserDTO> GetReportUserByIdAsync(int reportUserId)
    {
        var reportUser = await unitOfWork.ReportRepository.GetReportUserByIdAsync(reportUserId);
        if (reportUser == null) return null!;
        var reportUserDTO = mapper.Map<ReportUserDTO>(reportUser);
        return reportUserDTO;
    }

    public async Task<int> GetReportUserCountAsync()
    {
        return await unitOfWork.ReportRepository.GetReportUserCountAsync();
    }

    public async Task<bool> ReportBugAsync(ReportBugDTO reportBug)
    {
        var reportBugEntity = mapper.Map<ReportBug>(reportBug);
        unitOfWork.ReportRepository.ReportBug(reportBugEntity);
        return await Done();
    }

    public async Task<bool> ReportPostAsync(ReportPostDTO reportPost)
    {
        var reportPostEntity = mapper.Map<ReportPost>(reportPost);
        unitOfWork.ReportRepository.ReportPost(reportPostEntity);
        return await Done();
    }

    public async Task<bool> ReportUserAsync(ReportUserDTO reportUser)
    {
        var reportUserEntity = mapper.Map<ReportUser>(reportUser);
        unitOfWork.ReportRepository.ReportUser(reportUserEntity);
        return await Done();
    }

    public async Task<bool> ResolveReportBugAsync(int reportBugId)
    {
        var reportBug = await unitOfWork.ReportRepository.ResolveReportBugAsync(reportBugId);
        if(!reportBug) return false;
        var result = await unitOfWork.Complete();
        if(!result) return false;
        return true;

    }

    public async Task<bool> ResolveReportPostAsync(int reportPostId)
    {
        var reportPost = await unitOfWork.ReportRepository.ResolveReportPostAsync(reportPostId);
        if(!reportPost) return false;
        var result = await unitOfWork.Complete();
        if(!result) return false;
        return true;
    }

    public async Task<bool> ResolveReportUserAsync(int reportUserId)
    {
        var reportUser = await unitOfWork.ReportRepository.ResolveReportUserAsync(reportUserId);
        if(!reportUser) return false;
        var result = await unitOfWork.Complete();
        if(!result) return false;
        return true;
    }

    private async Task<bool> Done()
    {
        var result = await unitOfWork.Complete();
        return result;
    }
}
