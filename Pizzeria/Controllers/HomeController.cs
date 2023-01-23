using Microsoft.AspNetCore.Mvc;
using Pizzeria.Models;
using System.Diagnostics;

namespace Pizzeria.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    public IActionResult Privacy()
        {
            return View();
        }
    }
}