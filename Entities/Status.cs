namespace LostAndFound.Entities;

public class Status
{
    public int Id { get; set; }
    public bool Found = false;
    public string Location { get; set; } = string.Empty;
    public string? SpecificLocation;
    public DateTime PostedDate { get; set; } = DateTime.Now;
    public DateTime DateLost  { get; set; } = DateTime.Now;
    public DateTime DateFound { get; set; } = DateTime.Now;

}
