using System;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IPostService
{
    Task<List<PostDTO>?> GetAllPostsAsync();
    Task<string> GetPostTitleByIdAsync(int id);

    Task<int> CreatePostAsync(string username, PostDTO postDto, bool isValid);
    Task<bool> UpdatePostAsync(int id, PostDTO postDto, bool isValid);

    Task<PostDTO?> GetPostDetailsByIdAsync(int id);

}
