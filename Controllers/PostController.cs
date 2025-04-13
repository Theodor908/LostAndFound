using System.ComponentModel.DataAnnotations;
using LostAndFound.Entities;
using LostAndFound.Extensions;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;

[Authorize]
public class PostController(IUserService userService, ICategoryService categoryService, IPostService postService) : Controller
{

    public IActionResult CreateReport()
    {
        return View();
    }

    public async Task<IActionResult> ReportItem(int numberOfItems)
    {
        int userId = User.GetUserId();
        var model = new PostDTO
        {
            Id = userId,
            Title = string.Empty,
            Description = string.Empty,
            NumberOfItems = numberOfItems,
            Items = []
        };

        foreach (var i in Enumerable.Range(0, numberOfItems))
        {
            model.Items.Add(new ItemDTO
            {
                Name = string.Empty,
                Description = string.Empty,
                Country = string.Empty,
                City = string.Empty,
                Location = string.Empty
            });
        }

        ViewBag.Categories = await categoryService.GetAllCategoriesAsync();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(PostDTO postDto, string PostType)
    {
        if(ModelState.IsValid == false)
        {
            ViewBag.Categories = await categoryService.GetAllCategoriesAsync();
            return View("ReportItem", postDto);
        }

        var username = User.GetUsername();
        var result = await postService.CreatePostAsync(username, postDto, PostType, ModelState.IsValid);

        if (result == null)
        {
            ViewBag.Categories = await categoryService.GetAllCategoriesAsync();
            return View("ReportItem", postDto);
        }

        return RedirectToAction("Details", new { id = result.Id });
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var postDetails = await postService.GetPostDetailsByIdAsync(id);
        if (postDetails == null)
        {
            return NotFound();
        }
        return View(postDetails);
    }
}