using System;
using LostAndFound.Entities;
using LostAndFound.Helpers;

namespace LostAndFound.Interfaces;

public interface ICategoryRepository
{
    
    Task<List<Category>?> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<Category?> GetCategoryByNameAsync(string name);
    void DeleteCategory(Category category);
    Task DeleteCategory(int categoryId);
    void CreateCategory(Category category);
    void UpdateCategory(Category category);
    Task<int> GetCategoriesCountAsync();
    Task<PagedList<Category>?> GetAllCategoriesAsync(CategoryFilterParams categoryFilterParams);
}
