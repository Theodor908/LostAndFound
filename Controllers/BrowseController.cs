using System.Threading.Tasks;
using LostAndFound.Entities;
using LostAndFound.Helpers;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;

public class BrowseController(IItemRepository itemRepository) : Controller
{

    public async Task<IActionResult> Index(string status, string categoryName, string country, string city, string dateRange, string searchTerm, int pageNumber = 1)
    {
        Console.WriteLine($"Status: {status}, CategoryName: {categoryName}, Country: {country}, City: {city}, DateRange: {dateRange}, SearchTerm: {searchTerm}, PageNumber: {pageNumber}");
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

        var pagedItems = await itemRepository.GetFilteredItemsAsync(filterParams);
        var categories = await itemRepository.GetCategoriesWithItemCountsAsync();
        var countries = await itemRepository.GetDistinctCountriesAsync();
        var cities = await itemRepository.GetDistinctCitiesByCountryAsync(country ?? string.Empty);



        var viewModel = new BrowseDTO
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

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> GetCitiesByCountry(string country)
    {
        if (string.IsNullOrEmpty(country))
        {
            return Json(new { success = false });
        }

        var cities = await itemRepository.GetDistinctCitiesByCountryAsync(country);
        
        return Json(new { 
            success = true, 
            cities = cities 
        });
    }

}