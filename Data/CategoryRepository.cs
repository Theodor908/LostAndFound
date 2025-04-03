using System;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LostAndFound.Data;

public class CategoryRepository(DataContext dataContext) : ICategoryRepository
{
    public void AddCategoryAsync(Category category)
    {
        dataContext.Categories.Add(category);
    }

    public void DeleteCategoryAsync(Category category)
    {
        dataContext.Categories.Remove(category);
    }

    public async Task<IEnumerable<Category>?> GetCategoriesAsync()
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

    public void UpdateCategoryAsync(Category category)
    {
        dataContext.Entry(category).State = EntityState.Modified;
    }
}
