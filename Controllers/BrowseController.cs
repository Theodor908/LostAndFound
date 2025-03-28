using Microsoft.AspNetCore.Mvc;
using LostAndFound.Models;
using Microsoft.EntityFrameworkCore;
using LostAndFound.Data;

namespace LostAndFound.Controllers;

public class BrowseController : Controller
{
    private readonly DataContext _context;
    public BrowseController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(ItemBrowseViewModel searchModel)
    {   
        var query = _context.Items.AsQueryable();

        if(searchModel.Status != null)
        {
            query = query.Where(i => i.Status == searchModel.Status);
        }

        return View(items);
    }
    
}
