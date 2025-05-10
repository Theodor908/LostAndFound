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

    public async Task<PagedList<Post>> GetAllPostsAsync(PostFilterParams postFilterParams)
    {
        var query = dataContext.Posts
            .Include(p => p.AppUser)
            .AsQueryable();

        if (postFilterParams.IsActive.HasValue)
        {
            query = query.Where(p => p.IsActive == postFilterParams.IsActive.Value);
        }

        if (!string.IsNullOrEmpty(postFilterParams.SearchTerm))
        {
            var searchTerm = postFilterParams.SearchTerm.ToLower();
            query = query.Where(p =>
                p.Title.ToLower().Contains(searchTerm) ||
                p.Description.ToLower().Contains(searchTerm));
        }

        if (!string.IsNullOrEmpty(postFilterParams.PostType))
        {
            query = query.Where(p => postFilterParams.PostType! == p.PostType);
        }

        var totalCount = await query.CountAsync();

        if (!string.IsNullOrEmpty(postFilterParams.SortBy))
        {
            query = postFilterParams.SortBy.ToLower() switch
            {
                "date" => postFilterParams.SortDescending
                    ? query.OrderByDescending(p => p.CreatedAt)
                    : query.OrderBy(p => p.CreatedAt),
                "title" => postFilterParams.SortDescending
                    ? query.OrderByDescending(p => p.Title)
                    : query.OrderBy(p => p.Title),
                _ => postFilterParams.SortDescending
                    ? query.OrderByDescending(p => p.CreatedAt)
                    : query.OrderBy(p => p.CreatedAt)
            };
        }
        else
        {
            query = query.OrderByDescending(p => p.CreatedAt);
        }

        var posts = await query
            .Skip((postFilterParams.PageNumber - 1) * postFilterParams.PageSize)
            .Take(postFilterParams.PageSize)
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)postFilterParams.PageSize);
        return new PagedList<Post>
        {
            Items = posts,
            TotalCount = totalCount,
            PageSize = postFilterParams.PageSize,
            CurrentPage = postFilterParams.PageNumber,
            TotalPages = totalPages
        };
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

        if (filterParams.IsActive.HasValue)
        {
            query = query.Where(p => p.IsActive == filterParams.IsActive.Value);
        }

        if (!string.IsNullOrEmpty(filterParams.SearchTerm))
        {
            var searchTerm = filterParams.SearchTerm.ToLower();
            query = query.Where(p =>
                p.Title.ToLower().Contains(searchTerm) ||
                p.Description.ToLower().Contains(searchTerm));
        }

        var totalCount = await query.CountAsync();

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
            query = query.OrderByDescending(p => p.CreatedAt);
        }

        var posts = await query
            .Skip((filterParams.PageNumber - 1) * filterParams.PageSize)
            .Take(filterParams.PageSize)
            .ToListAsync();

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

    public Task<string?> GetPostTitleByIdAsync(int id)
    {
        var query = dataContext.Posts
            .Where(p => p.Id == id)
            .Select(p => p.Title);
        return query.FirstOrDefaultAsync();
        
    }

    public async Task<int> GetPostCountAsync()
    {
        return await dataContext.Posts.CountAsync();
    }

    public async Task<int> GetPostReportCountAsync()
    {
        return await dataContext.ReportPosts.CountAsync();
    }
}