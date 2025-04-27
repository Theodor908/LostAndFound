using System;
using LostAndFound.Helpers;
using LostAndFound.Models;

namespace LostAndFound.Interfaces;

public interface IBrowseService
{
    Task<PagedList<ItemDTO>> GetFilteredItemsAsync(ItemFilterParams filterParams);
    Task<List<CategoryDTO>> GetCategoriesWithItemCountsAsync();
    Task<List<string>> GetDistinctCountriesAsync();
    Task<List<string>> GetDistinctCitiesByCountryAsync(string country);
    Task<BrowseDTO> GetBrowseDTOAsync(string status, string categoryName, string country, string city, string dateRange, string searchTerm, int pageNumber = 1);
}
