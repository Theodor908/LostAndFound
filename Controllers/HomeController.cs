using Microsoft.AspNetCore.Mvc;

namespace LostAndFound.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}
