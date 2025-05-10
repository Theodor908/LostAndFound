using System;
using System.Threading.Tasks;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;

[Authorize(Roles = "Admin, Moderator")]
public class CategoryController(ICategoryService categoryService) : Controller
{

    public async Task<IActionResult> CategoryDetails(int id)
    {
        var categoryDTO = await categoryService.GetCategoryById(id);
        if (categoryDTO == null)
        {
            return NotFound("Category not found.");
        }
        return View(categoryDTO);
    }

    public IActionResult CreateCategory()
    {
        return View(new CategoryDTO());
    }
    [HttpPost]
    public async Task<IActionResult> CreateCategory(CategoryDTO categoryDTO)
    {
        if (!ModelState.IsValid)
        {
            return View(categoryDTO);
        }

        var result = await categoryService.CreateCategoryAsync(categoryDTO.Name);
        if (result == null)
        {
            return StatusCode(500, "Internal server error");
        }

        return RedirectToAction("CategoryDetails", new { id = result.Id });
    }

    public async Task<IActionResult> EditCategory(int id)
    {
        var categoryDTO = await categoryService.GetCategoryById(id);
        if (categoryDTO == null)
        {
            return NotFound("Category not found.");
        }
        return View(categoryDTO);
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(CategoryDTO categoryDTO)
    {
        if (!ModelState.IsValid)
        {
            return View(categoryDTO);
        }

        var result = await categoryService.UpdateCategoryAsync(categoryDTO.Id, categoryDTO.Name);
        if (!result)
        {
            return StatusCode(500, "Internal server error");
        }

        return RedirectToAction("CategoryDetails", new { id = categoryDTO.Id });
    } 

    public async Task<IActionResult> DeleteCategory(int id)
    {
        var result = await categoryService.DeleteCategoryAsync(id);
        if (!result)
        {
            return StatusCode(500, "Internal server error");
        }
        return RedirectToAction("CategoryList", "Admin");
    }
}
