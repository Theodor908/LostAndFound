using LostAndFound.Entities;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;
[Authorize]
public class PostController(IPostRepository postRepository) : Controller
{
    public IActionResult ReportItem()
    {
        var model = new PostDTO
        {
            Title = string.Empty,
            Description = string.Empty,
            Items = []
        };
        return View(model);
    }

    // [HttpPost]
    // public async Task<IActionResult> CreatePost()
    // {
        
    // }

    // public async Task<IActionResult> Details(int id)
    // {
    //     var post = await postRepository.GetPostWithItemsAndCommentsByIdAsync(id);
        
    //     if (post == null)
    //     {
    //         return NotFound();
    //     }

    //     // TO DO map to view model

    //     return View(viewModel);
    // }


}