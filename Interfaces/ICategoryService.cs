using System;
using LostAndFound.Helpers;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface ICategoryService
{
      Task<List<CategoryDTO>> GetAllCategoriesAsync();
      Task<CategoryDTO?> GetCategoryById(int id);
      Task<string> GetCategoryNameByIdAsync(int id);
      Task<CategoryDTO> CreateCategoryAsync(string categoryName);
      Task<bool> UpdateCategoryAsync(int categoryId, string newCategoryName);
      Task<bool> DeleteCategoryAsync(int categoryId);
      Task<int> GetCategoriesCountAsync();
      Task<CategoryListDTO> GetAllCategoriesAsync(CategoryFilterParams categoryFilterParams);
      
}
