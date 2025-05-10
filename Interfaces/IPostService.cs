using System;
using LostAndFound.Helpers;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IPostService
{
    Task<PostListDTO> GetAllPostsAsync(PostFilterParams filterParams);
    Task<string> GetPostTitleByIdAsync(int id);

    Task<PostDTO> ReducePostDTOItemListAsync(int itemIndex, int postId);

    Task<int> CreatePostAsync(string username, PostDTO postDto, bool isValid);
    Task<bool> UpdatePostAsync(int id, PostDTO postDto, bool isValid);

    Task<PostDTO?> GetPostDetailsByIdAsync(int id);

    Task<bool> DeletePostAsync(int id);
    Task<int> GetPostCountAsync();

}
