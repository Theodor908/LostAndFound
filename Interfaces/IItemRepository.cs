using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IItemRepository
{

    Task<Item?> GetItemByIdAsync(int id);
    void AddItem(Item item);
    void UpdateItemAsync(Item item);
    Task<bool> DeleteItemAsync(int id);
    
    Task<List<Item>> GetAllItemsAsync();
    Task<List<Item>> GetItemsByUserIdAsync(int userId, bool? isFound = null);
    Task<List<Item>> GetItemsByCategoryIdAsync(int categoryId);
    Task<List<Item>> GetItemsByCategoryNameAsync(string categoryName);
    Task<List<Item>> GetItemsByCountryAsync(string country);
    Task<List<Item>> GetItemsByCityAsync(string city);
    Task<List<Item>> GetItemsByPostIdAsync(int postId);
    
    Task<PagedList<Item>> GetFilteredItemsAsync(ItemFilterParams filterParams);
    
    Task<List<Category>> GetCategoriesWithItemCountsAsync();
    
    Task<List<string>> GetDistinctCountriesAsync();
    Task<List<string>> GetDistinctCitiesByCountryAsync(string country);
}