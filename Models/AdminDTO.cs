using System;

namespace LostAndFound.Models;

public class AdminDTO
{
    public int CountUsers { get; set; }
    public int CountPosts { get; set; }
    public int CountPostReports { get; set; }
    public int CountUserReports { get; set; }
    public int CountBugReports { get; set; }
    public int CountBans { get; set; }
    public int CountRoles { get; set; }
    public int CountCategories { get; set; }
    public int CountItems { get; set; }

}
