using System;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface ICategoryService
{
      Task<List<CategoryDTO>?> GetAllCategoriesAsync();
      Task<CategoryDTO?> GetCategoryById(int id);
      Task<string> GetCategoryNameByIdAsync(int id);
      
}
