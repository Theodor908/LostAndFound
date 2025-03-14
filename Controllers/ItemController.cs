using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LostAndFound.Models;

namespace LostAndFound.Controllers;

public class ItemController : Controller
{

    public IActionResult ReportLostItem()
    {
        return View();
    }
}
