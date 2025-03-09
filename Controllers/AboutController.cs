using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LostAndFound.Models;

namespace LostAndFound.Controllers;

public class AboutController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}
