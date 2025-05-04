using System;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class BugService(IUnitOfWork unitOfWork) : IBugService
{
    public Task<bool> CreateBugAsync(BugDTO bugDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBugAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<BugDTO>?> GetAllBugsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BugDTO?> GetBugByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetBugReportCountAsync()
    {
        return await unitOfWork.BugRepository.GetBugCountAsync();
    }

    public Task<bool> Solved(int id, bool isSolved)
    {
        throw new NotImplementedException();
    }
}
