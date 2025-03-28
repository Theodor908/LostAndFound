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
    public 
}
