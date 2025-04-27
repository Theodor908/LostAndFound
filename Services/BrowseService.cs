using System;
using AutoMapper;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;

namespace LostAndFound.Services;

public class BrowseService(IUnitOfWork unitOfWork, IMapper mapper) : IBrowseService
{
    public async Task<List<CategoryDTO>> GetCategoriesWithItemCountsAsync()
    {
        var categories = await unitOfWork.ItemRepository.GetCategoriesWithItemCountsAsync();
        var categoriesDTO = mapper.Map<List<CategoryDTO>>(categories);
        return categoriesDTO;
    }

    public Task<List<string>> GetDistinctCitiesByCountryAsync(string country)
    {
        var cities = unitOfWork.ItemRepository.GetDistinctCitiesByCountryAsync(country);
        return cities;
    }

    public async Task<List<string>> GetDistinctCountriesAsync()
    {
        var countries = await unitOfWork.ItemRepository.GetDistinctCountriesAsync();
        return countries;
    }

    public async Task<PagedList<ItemDTO>> GetFilteredItemsAsync(ItemFilterParams filterParams)
    {
        var pagedItems = await unitOfWork.ItemRepository.GetFilteredItemsAsync(filterParams);
        var pagedItemsDTO = mapper.Map<PagedList<ItemDTO>>(pagedItems);

        return pagedItemsDTO;
    }

    public async Task<BrowseDTO> GetBrowseDTOAsync(string status, string categoryName, string country, string city, string dateRange, string searchTerm, int pageNumber = 1)
    {
        var filterParams = new ItemFilterParams
        {
            Status = status,
            CategoryName = categoryName,
            Country = country,
            City = city,
            DateRange = dateRange,
            SearchTerm = searchTerm,
            PageNumber = pageNumber,
            PageSize = 12 
        };

        var pagedItems = await GetFilteredItemsAsync(filterParams);
        var categories = await GetCategoriesWithItemCountsAsync();
        var countries = await GetDistinctCountriesAsync();
        var cities = await GetDistinctCitiesByCountryAsync(country ?? string.Empty);

        return new BrowseDTO
        {
            ItemStatus = status ?? string.Empty,
            CategoryName = categoryName ?? string.Empty,
            Country = country ?? string.Empty,
            Countries = countries,
            City = city ?? string.Empty,
            Cities = cities,
            DateRange = dateRange ?? string.Empty,
            SearchTerm = searchTerm ?? string.Empty,
            Items = pagedItems,
            Categories = categories,
            FilterParams = filterParams
        };
    }
}
