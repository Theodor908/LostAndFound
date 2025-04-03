using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LostAndFound.Models;

namespace LostAndFound.Controllers
{
    public class ItemController : Controller
    {

        [HttpGet]
        public IActionResult ReportItem()
        {
            return View();
        }
    }
}