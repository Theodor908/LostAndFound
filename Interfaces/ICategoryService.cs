using System;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface ICategoryService
{
      Task<List<CategoryDTO>?> GetAllCategoriesAsync();
      Task<CategoryDTO?> GetCategoryById(int id);
      Task<string> GetCategoryNameByIdAsync(int id);
      void CreateCategory(string categoryName);
      Task<bool> UpdateCategoryAsync(int categoryId, string newCategoryName);
      Task DeleteCategory(int categoryId);
      Task<int> GetCategoryCountAsync();
      
}
