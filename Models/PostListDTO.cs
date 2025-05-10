using System;
using LostAndFound.Helpers;

namespace LostAndFound.Models;

public class PostListDTO
{
    public bool? IsActive { get; set; }
    public string? PostType { get; set; } = "";
    public string? SearchTerm { get; set; }
    public string? SortBy { get; set; } = "Title";
    public PagedList<PostDTO> PostList { get; set; } = new PagedList<PostDTO>();
}
