using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalHPMS.Models;
using FinalHPMS.Services;


namespace FinalHPMS.Controllers;


public class HomeController : Controller
{
    public HomeController()
    {

    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
