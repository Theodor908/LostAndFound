using System.Threading.Tasks;
using LostAndFound.Entities;
using LostAndFound.Interfaces;
using LostAndFound.Models;
using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;

public class PostsController(IPostRepository postRepository, IItemRepository itemRepository) : Controller
{

    public async Task<IActionResult> Details(int id)
    {
        var post = await postRepository.GetPostWithItemsAndCommentsByIdAsync(id);
        
        if (post == null)
        {
            return NotFound();
        }

        var viewModel = new PostDetailsViewModel
        {
            Post = post
        };

        return View(viewModel);
    }
}

public class PostDetailsViewModel
{
    public Post Post { get; set; } = null!;
}