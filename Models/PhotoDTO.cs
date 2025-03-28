using System;

namespace LostAndFound.Models;

public class PhotoDTO
{
    public required string Url { get; set; }
    public string? PublicId { get; set; }
}
