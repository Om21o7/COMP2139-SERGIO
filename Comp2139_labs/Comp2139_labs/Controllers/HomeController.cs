using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Comp2139_labs.Models;

namespace Comp2139_labs.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GeneralSearch(string searchType , string searchString)
    {
        if(searchType == "Projects")
        {
            return RedirectToAction("Search", "Project", new { searchString });
        }
        else if (searchType == "Tasks")
        {
            return RedirectToAction("Search", "Tasks", new { searchString });

        }
        return RedirectToAction("Index", "Home");

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult NotFound( int statusCode)
    {
        if(statusCode == 404)
        {
            return View("NotFound");
        }
        return View("Error");
    }
}

