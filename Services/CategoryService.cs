using System.Threading.Tasks;
using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using LostAndFound.Models;
namespace LostAndFound.Services
{
    public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
        public void CreateCategory(string categoryName)
        {
            Category category = new()
            {
                Name = categoryName
            };

            unitOfWork.CategoryRepository.CreateCategory(category);
        }

        public async Task DeleteCategory(int categoryId)
        {
            await unitOfWork.CategoryRepository.DeleteCategory(categoryId);
        }

        public async Task<List<CategoryDTO>?> GetAllCategoriesAsync()
        {
            List<Category>? categories = await unitOfWork.CategoryRepository.GetAllCategoriesAsync();

             List<CategoryDTO> categoryDTOs = mapper.Map<List<CategoryDTO>>(categories);

            return categoryDTOs;
        }

        public async Task<CategoryDTO?> GetCategoryById(int id)
        {
            Category? category = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);
            CategoryDTO categoryDTO = mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }

        public async Task<string> GetCategoryNameByIdAsync(int id)
        {
            Category? category = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return string.Empty;
            }
            return category.Name;
        }

        public async Task<bool> UpdateCategoryAsync(int categoryId, string newCategoryName)
        {
            Category? category = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
            {
                return false;
            }

            category.Name = newCategoryName;
            unitOfWork.CategoryRepository.UpdateCategory(category);
            return true;
        }

        public async Task<int> GetCategoryCountAsync()
        {
            return await unitOfWork.CategoryRepository.GetCategoryCountAsync();
        }
    }
}