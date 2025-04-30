using LostAndFound.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace LostAndFound.Controllers
{

    public class ItemController(IItemService itemService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ItemDetails(int id)
        {
            var itemDetails = await itemService.GetItemDetailsByIdAsync(id);
            if (itemDetails == null)
            {
                return NotFound();
            }
            return View(itemDetails);
        }
    }
}