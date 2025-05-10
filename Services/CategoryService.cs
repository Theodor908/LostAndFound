using System.Threading.Tasks;
using AutoMapper;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;
namespace LostAndFound.Services
{
    public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {

        public async Task<List<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await unitOfWork.CategoryRepository.GetAllCategoriesAsync();
            var categoryList = mapper.Map<List<CategoryDTO>>(categories);
            return categoryList;
        }
        public async Task CreateCategory(string categoryName)
        {
            Category category = new()
            {
                Name = categoryName
            };

            unitOfWork.CategoryRepository.CreateCategory(category);
            var result = await unitOfWork.Complete();
            if (!result)
            {
                throw new Exception("Failed to create category");
            }
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            await unitOfWork.CategoryRepository.DeleteCategory(categoryId);
            var result = await unitOfWork.Complete();
            if (!result)
            {
                throw new Exception("Failed to delete category");
            }
            return true;
        }

        public async Task<CategoryListDTO> GetAllCategoriesAsync(CategoryFilterParams categoryFilterParams)
        {
            var categories = await unitOfWork.CategoryRepository.GetAllCategoriesAsync(categoryFilterParams);
            var categoryList = mapper.Map<PagedList<CategoryDTO>>(categories);    
            var categoryListDTO = new CategoryListDTO
            {
                SearchTerm = categoryFilterParams.SearchTerm!,
                CategoryList = categoryList
            };
            return categoryListDTO;
        }

        public async Task<CategoryDTO?> GetCategoryById(int id)
        {
            Category? category = await unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return null;
            }
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
            var result = await unitOfWork.Complete();
            if (!result)
            {
                throw new Exception("Failed to update category");
            }
            return true;
        }

        public async Task<CategoryDTO> CreateCategoryAsync(string categoryName)
        {
            Category category = new()
            {
                Name = categoryName
            };

            unitOfWork.CategoryRepository.CreateCategory(category);
            var result = await unitOfWork.Complete();
            if (!result)
            {
                throw new Exception("Failed to create category");
            }

            return mapper.Map<CategoryDTO>(category);
        }

        public Task<int> GetCategoriesCountAsync()
        {
            return unitOfWork.CategoryRepository.GetCategoriesCountAsync();
        }
    }
}