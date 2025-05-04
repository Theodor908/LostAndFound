using System;
using LostAndFound.Entities;
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
        return await dataContext.Categories
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id);
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

    public async Task<int> GetCategoryCountAsync()
    {
        return await dataContext.Categories.CountAsync();
    }
}
