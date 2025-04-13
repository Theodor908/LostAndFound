using LostAndFound.Data;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Repositories;

public class PostRepository(DataContext dataContext) : IPostRepository
{
    public async Task<Post?> GetPostByIdAsync(int id)
    {
        return await dataContext.Posts
            .Include(p => p.AppUser)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Post>?> GetAllPostsAsync()
    {
        return await dataContext.Posts.ToListAsync();
    }

    public async Task<Post?> GetPostWithItemsAndCommentsByIdAsync(int id)
    {
        return await dataContext.Posts
            .Include(p => p.AppUser)
                .ThenInclude(u => u.Photo)
            .Include(p => p.Items)
                .ThenInclude(i => i.Category)
            .Include(p => p.Items)
                .ThenInclude(i => i.Photos)
            .Include(p => p.Comments)
                .ThenInclude(c => c.AppUser)
                    .ThenInclude(u => u.Photo)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Post>> GetPostsByUserIdAsync(int userId)
    {
        return await dataContext.Posts
            .Include(p => p.Items)
            .Where(p => p.AppUserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<PagedList<Post>> GetPostsAsync(PostFilterParams filterParams)
    {
        var query = dataContext.Posts
            .Include(p => p.AppUser)
            .Include(p => p.Items)
            .AsQueryable();

        // Apply active filter
        if (filterParams.IsActive.HasValue)
        {
            query = query.Where(p => p.IsActive == filterParams.IsActive.Value);
        }

        // Apply search term
        if (!string.IsNullOrEmpty(filterParams.SearchTerm))
        {
            var searchTerm = filterParams.SearchTerm.ToLower();
            query = query.Where(p =>
                p.Title.ToLower().Contains(searchTerm) ||
                p.Description.ToLower().Contains(searchTerm));
        }

        // Calculate total count before pagination
        var totalCount = await query.CountAsync();

        // Apply sorting
        if (!string.IsNullOrEmpty(filterParams.SortBy))
        {
            query = filterParams.SortBy.ToLower() switch
            {
                "date" => filterParams.SortDescending
                    ? query.OrderByDescending(p => p.CreatedAt)
                    : query.OrderBy(p => p.CreatedAt),
                "title" => filterParams.SortDescending
                    ? query.OrderByDescending(p => p.Title)
                    : query.OrderBy(p => p.Title),
                "comments" => filterParams.SortDescending
                    ? query.OrderByDescending(p => p.Comments.Count)
                    : query.OrderBy(p => p.Comments.Count),
                "items" => filterParams.SortDescending
                    ? query.OrderByDescending(p => p.Items.Count)
                    : query.OrderBy(p => p.Items.Count),
                _ => filterParams.SortDescending
                    ? query.OrderByDescending(p => p.CreatedAt)
                    : query.OrderBy(p => p.CreatedAt)
            };
        }
        else
        {
            // Default sort by newest
            query = query.OrderByDescending(p => p.CreatedAt);
        }

        // Apply pagination
        var posts = await query
            .Skip((filterParams.PageNumber - 1) * filterParams.PageSize)
            .Take(filterParams.PageSize)
            .ToListAsync();

        // Create paged result
        var totalPages = (int)Math.Ceiling(totalCount / (double)filterParams.PageSize);
        return new PagedList<Post>
        {
            Items = posts,
            TotalCount = totalCount,
            PageSize = filterParams.PageSize,
            CurrentPage = filterParams.PageNumber,
            TotalPages = totalPages
        };
    }

    public void AddPost(Post post)
    {
        dataContext.Posts.Add(post);
    }

    public void UpdatePost(Post post)
    {
        dataContext.Entry(post).State = EntityState.Modified;
    }

    public async Task<bool> DeletePostAsync(int id)
    {
        var post = await dataContext.Posts.FindAsync(id);
        if (post == null) return false;
        
        dataContext.Posts.Remove(post);
        return true;
    }
}