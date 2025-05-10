using System;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data;

public class CategoryRepository(DataContext dataContext) : ICategoryRepository
{
    public void CreateCategory(Category category)
    {
        dataContext.Categories.Add(category);
    }

    public void DeleteCategory(Category category)
    {
        dataContext.Categories.Remove(category);
    }

    public async Task DeleteCategory(int categoryId)
    {
        var category = await dataContext.Categories.FindAsync(categoryId);
        if (category != null)
        {
            dataContext.Categories.Remove(category);
        }
        else
        {
            throw new Exception($"Category with ID {categoryId} not found.");
        }
    }

    public async Task<List<Category>?> GetAllCategoriesAsync()
    {
        return await dataContext.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        var category = await dataContext.Categories
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id);
        if(category == null)
        {
            return null;
        }
        category.ItemCount = await GetCategoryByIdItemCountAsync(id);
        dataContext.Entry(category).State = EntityState.Modified;
        return category;
    }

    public Task<Category?> GetCategoryByNameAsync(string name)
    {
        return dataContext.Categories
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Name == name);
    }

    public void UpdateCategory(Category category)
    {
        dataContext.Entry(category).State = EntityState.Modified;
    }

    public async Task<int> GetCategoriesCountAsync()
    {
        return await dataContext.Categories.CountAsync();
    }

    private async Task<int> GetCategoryByIdItemCountAsync(int id)
    {
        var category = await dataContext.Categories
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id);

        return category?.Items.Count ?? 0;
    }

    public async Task<PagedList<Category>?> GetAllCategoriesAsync(CategoryFilterParams categoryFilterParams)
    {
        var query = dataContext.Categories.AsQueryable();

        if (!string.IsNullOrEmpty(categoryFilterParams.SearchTerm))
        {
            query = query.Where(c => c.Name.ToLower().Contains(categoryFilterParams.SearchTerm.ToLower()));
        }

        var category = await query
            .Skip((categoryFilterParams.PageNumber - 1) * categoryFilterParams.PageSize)
            .Take(categoryFilterParams.PageSize)
            .ToListAsync();

        var totalCount = await query.CountAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)categoryFilterParams.PageSize);
        return new PagedList<Category>
        {
            Items = category,
            TotalCount = totalCount,
            PageSize = categoryFilterParams.PageSize,
            CurrentPage = categoryFilterParams.PageNumber,
            TotalPages = totalPages
        };

    }
}
