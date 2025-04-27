using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LostAndFound.Entities;
[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public string? PublicId { get; set; }

    // Navigation properties
    public int? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public int? ItemId { get; set; }
    public Item? Item { get; set; }
}
    