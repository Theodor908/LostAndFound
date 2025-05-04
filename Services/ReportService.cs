using System;
using System.Threading.Tasks;
using AutoMapper;
using LostAndFound.Entities;
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

    public async Task<List<ReportBugDTO>?> GetAllReportBugsAsync()
    {
        var reportBugs = await unitOfWork.ReportRepository.GetAllReportBugsAsync();
        if (reportBugs == null) return null!;
        var reportBugsDTO = mapper.Map<List<ReportBugDTO>>(reportBugs);
        return reportBugsDTO;
    }

    public async Task<List<ReportPostDTO>?> GetAllReportPostsAsync()
    {
        var reportPosts = await unitOfWork.ReportRepository.GetAllReportPostsAsync();
        if (reportPosts == null) return null!;
        var reportPostsDTO = mapper.Map<List<ReportPostDTO>>(reportPosts);
        return reportPostsDTO;
    }

    public async Task<List<ReportUserDTO>?> GetAllReportUsersAsync()
    {
        var reportUsers = await unitOfWork.ReportRepository.GetAllReportUsersAsync();
        if (reportUsers == null) return null!;
        var reportUsersDTO = mapper.Map<List<ReportUserDTO>>(reportUsers);
        return reportUsersDTO;
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
        if(reportBug) return true;
        return false;

    }

    public async Task<bool> ResolveReportPostAsync(int reportPostId)
    {
        var reportPost = await unitOfWork.ReportRepository.ResolveReportPostAsync(reportPostId);
        if(reportPost) return true;
        return false;
    }

    public async Task<bool> ResolveReportUserAsync(int reportUserId)
    {
        var reportUser = await unitOfWork.ReportRepository.ResolveReportUserAsync(reportUserId);
        if(reportUser) return true;
        return false;
    }

    private async Task<bool> Done()
    {
        var result = await unitOfWork.Complete();
        return result;
    }
}
