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
using Microsoft.AspNetCore.Authorization;

namespace FinalHPMS.Controllers
{
    [Authorize(Roles = "Administrator,Employee,Supervisor")]
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICommunityService _communityService;
        public ProductController(IProductService productService, ICommunityService communityService)
        {
            _productService = productService;
            _communityService = communityService;
        }

        // GET: Product
        public IActionResult Index(string filter)
        {
            var productContext = _productService.GetAll(filter);
            return View(productContext);
        }

        // GET: Product/Details/5
        public IActionResult Details(int? id)
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
            var communityList = _communityService.GetAll();
            ViewData["Communities"] = new SelectList(communityList.Communities.ToList()
            .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }), "Value", "Text");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product, ProductCreateViewModel productCreateViewModel)
        {
            ModelState.Remove("Communities");
            if (ModelState.IsValid)
            {
                var model = new Product()
                {
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

        // GET: Product/Edit/5
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator,Supervisor")]
        public IActionResult Delete(int? id)
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
        [Authorize(Roles = "Administrator,Supervisor")]
        public IActionResult DeleteConfirmed(int id)
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
