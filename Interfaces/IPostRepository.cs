using AspNetCoreGeneratedDocument;
using LostAndFound.Entities;
using LostAndFound.Helpers;

namespace LostAndFound.Interfaces;

public interface IPostRepository
{
    Task<Post?> GetPostByIdAsync(int id);
    Task<string?> GetPostTitleByIdAsync(int id);
    Task<PagedList<Post>> GetAllPostsAsync(PostFilterParams filterParams);
    Task<List<Post>> GetPostsByUserIdAsync(int userId);
    Task<Post?> GetPostWithItemsAndCommentsByIdAsync(int id);
    Task<PagedList<Post>> GetPostsAsync(PostFilterParams filterParams);
    void AddPost(Post post);
    void UpdatePost(Post post);
    Task<bool> DeletePostAsync(int id);
    Task<int> GetPostCountAsync();
}