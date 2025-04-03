using System;
using LostAndFound.Entities;

namespace LostAndFound.Interfaces;

public interface ICategoryRepository
{
    
    Task<IEnumerable<Category>?> GetCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<Category?> GetCategoryByNameAsync(string name);
    void AddCategoryAsync(Category category);
    void UpdateCategoryAsync(Category category);
    void DeleteCategoryAsync(Category category);

}
