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
        
        SaveDraftToTempData(model);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(PostDTO postDTO)
    {

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await categoryService.GetAllCategoriesAsync();
            return View("PostCreate", postDTO);
        }

        var username = User.GetUsername();
        var result = await postService.CreatePostAsync(username, postDTO, ModelState.IsValid);

        if (result < 0)
        {
            ViewBag.Categories = await categoryService.GetAllCategoriesAsync();
            return View("PostCreate", postDTO);
        }
        
        TempData.Remove(TempDataKey);

        return RedirectToAction("PostDetails", new { id = result });
    }
    
    [HttpGet]
    public async Task<IActionResult> PostDetails(int id)
    {
        var postDetails = await postService.GetPostDetailsByIdAsync(id);
        if (postDetails == null)
        {
            return NotFound();
        }
        var userDetails = await userService.GetUserByIdAsync(postDetails.AppUserId);
        if (userDetails == null)
        {
            return NotFound();
        }
        PostDetailsDTO postDetailsDTO = new()
        {
            Post = postDetails,
            User = userDetails
        };
        return View(postDetailsDTO);
    }
    [HttpGet] 
    public async Task<IActionResult> PostUpdate(int id)
    {
        var postDetails = await postService.GetPostDetailsByIdAsync(id);
        if (postDetails == null)
        {
            return NotFound();
        }
        
        var userDetails = await userService.GetUserByIdAsync(postDetails.AppUserId);
        if (userDetails == null)
        {
            return NotFound();
        }
        
        PostDTO postDTO = new()
        {
            Id = postDetails.Id,
            Title = postDetails.Title,
            Description = postDetails.Description,
            PostType = postDetails.PostType,
            Items = postDetails.Items
        };
        
        ViewBag.Categories = await categoryService.GetAllCategoriesAsync();
        
        return View(postDTO);
    }

    [HttpPost]
    public async Task<IActionResult> PostUpdate(int id, PostDTO postDTO)
    {
        if (id != postDTO.Id)
        {
            return BadRequest("Post ID mismatch");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = await categoryService.GetAllCategoriesAsync();
            return View("PostUpdate", postDTO);
        }

        var result = await postService.UpdatePostAsync(id, postDTO, ModelState.IsValid);

        if (!result)
        {
            return BadRequest("Failed to update post");
        }

        return RedirectToAction("PostDetails", new { id = id });
    }


    [HttpGet]
    public async Task<IActionResult> GetItemTemplate(int index, string postType)
    {

        var draft = GetDraftFromTempData();
        
        if (draft != null)
        {
            while (draft.Items.Count <= index)
            {
                draft.Items.Add(new ItemDTO());
            }
            
            if (draft.PostType != postType)
            {
                draft.PostType = postType;
            }
            
            SaveDraftToTempData(draft);
        }
        
        var categories = await categoryService.GetAllCategoriesAsync();
        ViewBag.Categories = categories;
        ViewBag.ItemIndex = index;
        ViewBag.PostType = postType;
        
        return PartialView("_ItemPartialView");
    }

    [HttpPost]
    public IActionResult DeleteItem(int index)
    {

        var draft = GetDraftFromTempData();
        
        if (draft != null && draft.Items.Count > index)
        {

            draft.Items.RemoveAt(index);

            if (draft.Items.Count == 0)
            {
                draft.Items.Add(new ItemDTO());
            }
            
            SaveDraftToTempData(draft);
            
            return Json(new { success = true, newCount = draft.Items.Count });
        }
        
        return Json(new { success = false, message = "Item not found or cannot be deleted" });
    }
    
    [HttpPost]
    public IActionResult UpdatePostType(string postType)
    {
        var draft = GetDraftFromTempData();
        
        if (draft != null)
        {

            draft.PostType = postType;
            
            SaveDraftToTempData(draft);
            
            return Json(new { success = true });
        }
        
        return Json(new { success = false });
    }

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