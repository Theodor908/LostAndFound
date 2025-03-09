using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LostAndFound.Models;

namespace LostAndFound.Controllers;

public class AccountController : Controller
{

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult Profile()
    {
        return View();
    }
}
