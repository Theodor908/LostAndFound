using System;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IPostService
{
    Task<List<PostDTO>?> GetAllPostsAsync();

    Task<PostCreationDTO?> CreatePostAsync(string username, PostDTO postDto, string postType, bool isValid);

    Task<PostDTO?> GetPostDetailsByIdAsync(int id);

}
