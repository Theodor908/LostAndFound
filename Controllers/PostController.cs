using System.ComponentModel.DataAnnotations;
using LostAndFound.Extensions;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LostAndFound.Controllers;

[Authorize]
public class PostController(IUserService userService, ICategoryService categoryService, IPostService postService) : Controller
{
    private const string TempDataKey = "CurrentPostDraft";
    
    [HttpGet]
    public async Task<IActionResult> PostCreate()
    {
        int userId = User.GetUserId();
        var model = new PostDTO
        {
            Id = userId,
            Title = string.Empty,
            Description = string.Empty,
            PostType = "Lost", 
            Items = [new ItemDTO()]
        };
        
        // Store the initial model in TempData
        SaveDraftToTempData(model);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(PostDTO postDTO)
    {
        postDTO.Items = postDTO.Items.Where(i => !string.IsNullOrWhiteSpace(i.Name)).ToList();
        
        if (!postDTO.Items.Any())
        {
            ModelState.AddModelError("Items", "At least one item is required");
        }
        
        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        Console.WriteLine("ModelState Errors: " + string.Join(", ", errors));
        foreach (var error in errors)
        {
            Console.WriteLine(error);
        }
        Console.WriteLine("ModelState Error Count: " + ModelState.ErrorCount);

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await categoryService.GetAllCategoriesAsync();
            return View("ReportItem", postDTO);
        }

        var username = User.GetUsername();
        var result = await postService.CreatePostAsync(username, postDTO, ModelState.IsValid);

        if (result == null)
        {
            ViewBag.Categories = await categoryService.GetAllCategoriesAsync();
            return View("ReportItem", postDTO);
        }
        
        // Clear the draft data after successful submission
        TempData.Remove(TempDataKey);

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

    // Add this new action to handle AJAX requests for new item templates
    [HttpGet]
    public async Task<IActionResult> GetItemTemplate(int index, string postType)
    {
        // Get the current draft from TempData
        var draft = GetDraftFromTempData();
        
        // If we have a draft but not enough items, add a new one
        if (draft != null)
        {
            // Ensure we have enough items for the requested index
            while (draft.Items.Count <= index)
            {
                draft.Items.Add(new ItemDTO());
            }
            
            // Update postType if needed
            if (draft.PostType != postType)
            {
                draft.PostType = postType;
            }
            
            // Save the updated draft back to TempData
            SaveDraftToTempData(draft);
        }
        
        // Set up the ViewBag for the template
        var categories = await categoryService.GetAllCategoriesAsync();
        ViewBag.Categories = categories;
        ViewBag.ItemIndex = index;
        ViewBag.PostType = postType;
        
        return PartialView("_ItemPartialView");
    }

    [HttpPost]
    public IActionResult DeleteItem(int index)
    {
        // Get the current draft from TempData
        var draft = GetDraftFromTempData();
        
        if (draft != null && draft.Items.Count > index)
        {
            // Remove the item at the specified index
            draft.Items.RemoveAt(index);
            
            // Make sure we still have at least one item
            if (draft.Items.Count == 0)
            {
                draft.Items.Add(new ItemDTO());
            }
            
            // Save the updated draft back to TempData
            SaveDraftToTempData(draft);
            
            return Json(new { success = true, newCount = draft.Items.Count });
        }
        
        return Json(new { success = false, message = "Item not found or cannot be deleted" });
    }
    
    [HttpPost]
    public IActionResult UpdatePostType(string postType)
    {
        // Get the current draft from TempData
        var draft = GetDraftFromTempData();
        
        if (draft != null)
        {
            // Update the post type
            draft.PostType = postType;
            
            // Save the updated draft back to TempData
            SaveDraftToTempData(draft);
            
            return Json(new { success = true });
        }
        
        return Json(new { success = false });
    }
    
    // Helper method to retrieve the draft from TempData
    private PostDTO GetDraftFromTempData()
    {
        if (TempData.TryGetValue(TempDataKey, out var jsonData) && jsonData is string json)
        {
            TempData.Keep(TempDataKey);
            try
            {
                return JsonSerializer.Deserialize<PostDTO>(json);
            }
            catch
            {
                return null;
            }
        }
        
        return null;
    }
    
    private void SaveDraftToTempData(PostDTO draft)
    {
        if (draft != null)
        {
            var json = JsonSerializer.Serialize(draft);
            TempData[TempDataKey] = json;
        }
    }
}