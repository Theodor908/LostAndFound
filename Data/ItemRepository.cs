using LostAndFound.Data;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly DataContext _context;

    public ItemRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Item?> GetItemByIdAsync(int id)
    {
        return await _context.Items
            .Include(i => i.Category)
            .Include(i => i.Photos)
            .Include(i => i.AppUser)
            .Include(i => i.Post)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public void AddItem(Item item)
    {
        _context.Items.Add(item);
    }

    public void UpdateItemAsync(Item item)
    {
        _context.Entry(item).State = EntityState.Modified;
    }

    public async Task<bool> DeleteItemAsync(Item item)
    {
        if (item == null) return false;
        
        _context.Items.Remove(item);
        return true;
    }

    public async Task<List<Item>> GetAllItemsAsync()
    {
        return await _context.Items
            .Include(i => i.Category)
            .Include(i => i.Photos)
            .Include(i => i.Post)
            .ToListAsync();
    }

    public async Task<List<Item>> GetItemsByUserIdAsync(int userId, bool? isFound = null)
    {
        var query = _context.Items
            .Include(i => i.Category)
            .Include(i => i.Photos)
            .Include(i => i.Post)
            .Where(i => i.AppUserId == userId);

        if (isFound.HasValue)
        {
            query = query.Where(i => i.IsFound == isFound.Value);
        }

        return await query.ToListAsync();
    }

    public async Task<List<Item>> GetItemsByCategoryIdAsync(int categoryId)
    {
        return await _context.Items
            .Include(i => i.Category)
            .Include(i => i.Photos)
            .Include(i => i.Post)
            .Where(i => i.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<List<Item>> GetItemsByCategoryNameAsync(string categoryName)
    {
        return await _context.Items
            .Include(i => i.Category)
            .Include(i => i.Photos)
            .Include(i => i.Post)
            .Where(i => i.Category.Name.ToLower() == categoryName.ToLower())
            .ToListAsync();
    }

    public async Task<List<Item>> GetItemsByCountryAsync(string country)
    {
        return await _context.Items
            .Include(i => i.Category)
            .Include(i => i.Photos)
            .Include(i => i.Post)
            .Where(i => i.Country.ToLower() == country.ToLower())
            .ToListAsync();
    }

    public async Task<List<Item>> GetItemsByCityAsync(string city)
    {
        return await _context.Items
            .Include(i => i.Category)
            .Include(i => i.Photos)
            .Include(i => i.Post)
            .Where(i => i.City.ToLower() == city.ToLower())
            .ToListAsync();
    }

    public async Task<List<Item>> GetItemsByPostIdAsync(int postId)
    {
        return await _context.Items
            .Include(i => i.Category)
            .Include(i => i.Photos)
            .Where(i => i.PostId == postId)
            .ToListAsync();
    }

    public async Task<PagedList<Item>> GetFilteredItemsAsync(ItemFilterParams filterParams)
    {
        Console.WriteLine($"FilterParams: {filterParams.Status}, {filterParams.CategoryName}, {filterParams.Country}, {filterParams.City}, {filterParams.DateRange}, {filterParams.SearchTerm}");
        var query = _context.Items
            .Include(i => i.Category)
            .Include(i => i.Photos)
            .Include(i => i.Post)  
            .Include(i => i.AppUser)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filterParams.Status))
        {
            switch (filterParams.Status.ToLower())
            {
                case "lost":
                    query = query.Where(i => !i.IsFound);
                    break;
                case "found":
                    query = query.Where(i => i.IsFound);
                    break;
                case "claimed":
                    query = query.Where(i => i.IsClaimed);
                    break;
            }
        }
        Console.WriteLine($"Category: {filterParams.CategoryName}");

        if (!string.IsNullOrEmpty(filterParams.CategoryName))
        {
            query = query.Where(i => i.Category.Name == filterParams.CategoryName);
        }

        if (!string.IsNullOrEmpty(filterParams.Country))
        {
            query = query.Where(i => i.Country == filterParams.Country);
        }

        if (!string.IsNullOrEmpty(filterParams.City))
        {
            query = query.Where(i => i.City == filterParams.City);
        }

        if (!string.IsNullOrEmpty(filterParams.DateRange))
        {
            var today = DateTime.Today;
            switch (filterParams.DateRange.ToLower())
            {
                case "today":
                    query = query.Where(i => i.PostedAt.Date == today);
                    break;
                case "week":
                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                    query = query.Where(i => i.PostedAt.Date >= startOfWeek);
                    break;
                case "month":
                    var startOfMonth = new DateTime(today.Year, today.Month, 1);
                    query = query.Where(i => i.PostedAt.Date >= startOfMonth);
                    break;
            }
        }

        if (!string.IsNullOrEmpty(filterParams.SearchTerm))
        {
            var searchTerm = $"%{filterParams.SearchTerm}%"; 
            query = query.Where(i =>
                EF.Functions.Like(i.Name, searchTerm) ||
                EF.Functions.Like(i.Description, searchTerm) ||
                EF.Functions.Like(i.Location, searchTerm) ||
                EF.Functions.Like(i.Post.Title, searchTerm));
        }

        var totalCount = await query.CountAsync();

        if (!string.IsNullOrEmpty(filterParams.SortBy))
        {
            query = filterParams.SortBy.ToLower() switch
            {
                "date" => filterParams.SortDescending
                    ? query.OrderByDescending(i => i.FoundAt)
                    : query.OrderBy(i => i.FoundAt),
                "name" => filterParams.SortDescending
                    ? query.OrderByDescending(i => i.Name)
                    : query.OrderBy(i => i.Name),
                "post" => filterParams.SortDescending
                    ? query.OrderByDescending(i => i.Post.CreatedAt)
                    : query.OrderBy(i => i.Post.CreatedAt),
                _ => filterParams.SortDescending
                    ? query.OrderByDescending(i => i.Id)
                    : query.OrderBy(i => i.Id)
            };
        }
        else
        {
            query = query.OrderByDescending(i => i.Id);
        }

        var items = await query
            .Skip((filterParams.PageNumber - 1) * filterParams.PageSize)
            .Take(filterParams.PageSize)
            .ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)filterParams.PageSize);
        return new PagedList<Item>
        {
            Items = items,
            TotalCount = totalCount,
            PageSize = filterParams.PageSize,
            CurrentPage = filterParams.PageNumber,
            TotalPages = totalPages
        };
    }

    public async Task<List<Category>> GetCategoriesWithItemCountsAsync()
    {
        return await _context.Categories
            .Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                ItemCount = c.Items.Count
            })
            .ToListAsync();
    }

    public async Task<List<string>> GetDistinctCountriesAsync()
    {
        return await _context.Items
            .Select(i => i.Country)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<string>> GetDistinctCitiesByCountryAsync(string country)
    {
        if(string.IsNullOrEmpty(country))
        {
            return new List<string>();
        }
        return await _context.Items
            .Where(i => i.Country.ToLower() == country.ToLower())
            .Select(i => i.City)
            .Distinct()
            .ToListAsync();
    }
}