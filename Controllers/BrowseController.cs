using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;

public class BrowseController(IBrowseService browseService) : Controller
{

    public async Task<IActionResult> Index(string status, string categoryName, string country, string city, string dateRange, string searchTerm, int pageNumber = 1)
    {
        BrowseDTO viewModel = await browseService.GetBrowseDTOAsync(status, categoryName, country, city, dateRange, searchTerm, pageNumber);
        return View(viewModel);
        // browse
    }

    [HttpGet]
    public async Task<IActionResult> GetCitiesByCountry(string country)
    {
        if (string.IsNullOrEmpty(country))
        {
            return Json(new { success = false });
        }

        var cities = await browseService.GetDistinctCitiesByCountryAsync(country);
        
        return Json(new { 
            success = true, 
            cities = cities 
        });
    }

}