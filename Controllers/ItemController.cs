using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LostAndFound.Models;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using LostAndFound.Interfaces;

namespace LostAndFound.Controllers;

public class ItemController(IWebHostEnvironment webHostEnvironment, IPhotoService photoService) : Controller
{

    public IActionResult ReportLostItem()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ReportLost(ItemDTO item, IFormFile itemPhoto)
    {
        if(ModelState.IsValid)
        {
            if(itemPhoto != null)
            {
                var result = await photoService.UploadPhotoAsync(itemPhoto);
                if(result != null)
                {
                    item.Photo.PublicId = result.PublicId;
                    item.Photo.Url = result.SecureUrl.AbsoluteUri;
                }

            }
        }
        return View(item);
    }
}
