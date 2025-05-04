using System;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IBugService
{

    Task<List<BugDTO>?> GetAllBugsAsync();
    Task<BugDTO?> GetBugByIdAsync(int id);
    Task<bool> CreateBugAsync(BugDTO bugDto);
    Task<bool> DeleteBugAsync(int id);
    Task<bool> Solved(int id, bool isSolved);
    Task<int> GetBugReportCountAsync();

}
