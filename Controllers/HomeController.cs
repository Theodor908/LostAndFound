using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LostAndFound.Models;

namespace LostAndFound.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}
