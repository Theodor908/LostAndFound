using System;
using LostAndFound.Entities;

namespace LostAndFound.Interfaces;

public interface IBugRepository
{
    Task<List<ReportBug>?> GetAllBugsAsync();
    Task<ReportBug?> GetBugByIdAsync(int id);
    void CreateBug(ReportBug bug);
    void UpdateBug(ReportBug bug);
    void DeleteBug(ReportBug bug);
    Task DeleteBug(int bugId);
    Task<int> GetBugCountAsync();
    Task<bool> Solved(int id, bool isSolved);
}
