using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using LostAndFound.Models;
namespace LostAndFound.Services
{
    public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
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
    }
}