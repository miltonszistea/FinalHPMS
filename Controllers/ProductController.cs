using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalHPMS.Models;
using FinalHPMS.Services;

namespace FinalHPMS.Controllers;

public class ProductController : Controller
{
    public ProductController()
    {
    }

    public IActionResult Index()
    {   
        var model = new List<Product>();
        model = ProductService.GetAll();

        return View(model);
    }

    public IActionResult Detail(string id)
    {
        var model = ProductService.Get(id);

        return View(model);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product){
        if(!ModelState.IsValid){
            return RedirectToAction("Create");
        }

        ProductService.Add(product);

        return RedirectToAction("Index");
    }
}