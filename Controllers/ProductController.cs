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
       
        private ITicketService _ticketService;
        public ProductController(ICommunityService communityService, IProductService productService, ITicketService ticketService)
        {
            _communityService = communityService;
            _productService = productService;
            _ticketService = ticketService;
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
            if(product.Stock < 1)
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
            
            ModelState.Remove("Communities");
            if (ModelState.IsValid)
            {
                var model = new Product()
                {
                    Name = productCreateViewModel.Name,
                    Price = productCreateViewModel.Price,
                    ProductCategory = productCreateViewModel.Category,
                    WeightKg = productCreateViewModel.WeightKg,
                    ShippingAvailable = productCreateViewModel.ShippingAvailable,
                    Dimension = productCreateViewModel.Dimension,
                    Stock = productCreateViewModel.Stock,
                };
                _productService.Create(model, productCreateViewModel.CommunityIds);
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
            var communityList = _communityService.GetAll();
             ViewData["Communities"] = new SelectList(communityList.Communities.ToList()
            .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }), "Value", "Text");
            List<int> communityId = product.ProductCommunities.Select(x=>x.CommunityId).ToList();
            var model = new ProductCreateViewModel(){
                Id = product.Id,
                Name = product.Name,
                Category = product.ProductCategory,
                WeightKg = product.WeightKg,
                Dimension = product.Dimension,
                ShippingAvailable = product.ShippingAvailable,
                Stock = product.Stock,
                Price = product.Price,
                CommunityIds = communityId
            };

            return View(model);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(ProductCreateViewModel productModel)
        {
            if (productModel.Id == 0)
            {
                return NotFound();
            }
            if (productModel.CommunityIds == null)
                {
                    var communityList = _communityService.GetAll();
                    ViewData["Communities"] = new SelectList(communityList.Communities.ToList()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString()
                    }), "Value", "Text");
                    ModelState.AddModelError("Total", "El total no puede ser cero. Agregue productos válidos.");
                }

            ModelState.Remove("Communities");
            if (ModelState.IsValid)
            {
                var product = new Product(){
                Id = productModel.Id,
                Name = productModel.Name,
                ProductCategory = productModel.Category,
                WeightKg = productModel.WeightKg,
                Dimension = productModel.Dimension,
                ShippingAvailable = productModel.ShippingAvailable,
                Stock = productModel.Stock,
                Price = productModel.Price
                };
                var communityIds = productModel.CommunityIds;
                _productService.Update(product, communityIds);
                return RedirectToAction(nameof(Index));
            }
            return View(productModel);
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

        public IActionResult Tickets(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productTicketViewModel = _productService.GetDetails(id);

            if (productTicketViewModel == null)
            {
                return NotFound();
            }

            var model = new ProductTicketViewModel
            {
                Name = productTicketViewModel.Name,
                Tickets = _ticketService.GetTicketsByProductId(id)
            };

            return View(model);
        }

    }
}
