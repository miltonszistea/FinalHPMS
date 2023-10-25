using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalHPMS.Data;
using FinalHPMS.Models;
using FinalHPMS.ViewModels;
using FinalHPMS.Services;

namespace FinalHPMS.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Product
        public async Task<IActionResult> Index(string filter)
        {
            var productContext = _productService.GetAll(filter);

            return View(productContext);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productDetailViewModel = _productService.GetDetails(id.Value);

            if (productDetailViewModel == null)
            {
                return NotFound();
            }

            return View(productDetailViewModel);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, ProductCreateViewModel productCreateViewModel)
        { 
            // ModelState.Remove("CommunityId");
            ModelState.Remove("Communities");
            if (ModelState.IsValid)
            {
                    var model = new Product() {
                    Name = productCreateViewModel.Name,
                    Price = productCreateViewModel.Price,
                    Category = productCreateViewModel.Category,
                    WeightKg = productCreateViewModel.WeightKg,
                    ShippingAvailable = productCreateViewModel.ShippingAvailable,
                    Dimension = productCreateViewModel.Dimension,
                    Stock = productCreateViewModel.Stock,  
                    };
                _productService.Create(model);
                return RedirectToAction(nameof(Index));
            }            
            return View(productCreateViewModel);
        }


        // // [HttpPost]
        // // [ValidateAntiForgeryToken]
        // // public async Task<IActionResult> Create([Bind("Id,Name,Price,Category,WeightKg,ShippingAvailable,Dimension,Stock, Communities, CommunityId")] Product product)
        // // {
        // //     if (ModelState.IsValid)
        // //     {
        // //         _context.Add(product);
        // //         await _context.SaveChangesAsync();
        // //         return RedirectToAction(nameof(Index));
        // //     }
        // //     return View(product);
        // // }

        // GET: Product/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetDetails(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Price,Category,WeightKg,ShippingAvailable,Dimension,Stock")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Communities");
            if (ModelState.IsValid)
            {
                _productService.Update(product, id);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetDetails(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var product = _productService.GetProduct(id);
            if (product != null)
            {
                _productService.Delete(product);
            }
            return RedirectToAction(nameof(Index));
        }

        // private bool ProductExists(int id)
        // {
        //   return _productService.GetDProduct(id) != null;
        // }
    }
}
