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
            var communityList = _communityService.GetAll();
            ViewData["Communities"] = new SelectList(communityList.Communities.ToList()
            .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }), "Value", "Text");

            if(product.Stock < 1)
            {
                ModelState.AddModelError("Stock", "La cantidad debe ser mayor o igual a 1");
                return View();
            }
            
            if (!IsValidProductName(productCreateViewModel.Name))
            {
                ModelState.AddModelError("Name", "El nombre debe contener solo letras, números y espacios, y tener un máximo de 50 caracteres.");
            }

            if (!IsValidProductPrice(productCreateViewModel.Price))
            {
                ModelState.AddModelError("Price", "El precio debe estar entre 1 y 999999999.");
            }

            if (!IsValidProductWeight(productCreateViewModel.WeightKg))
            {
                ModelState.AddModelError("WeightKg", "El peso debe estar entre 1 y 999999.");
            }

            if (!IsValidProductSize(productCreateViewModel.Dimension))
            {
                ModelState.AddModelError("Dimension", "El formato del tamaño no es válido. Debe ser en el formato 'n1xn2xn3'.");
            }

            if (!IsValidProductStock(productCreateViewModel.Stock))
            {
                ModelState.AddModelError("Stock", "El stock debe estar entre 1 y 999999999.");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(ProductCreateViewModel productModel)
        {
            var communityList = _communityService.GetAll();
            ViewData["Communities"] = new SelectList(communityList.Communities.ToList()
            .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }), "Value", "Text");

            if (productModel.Id == 0)
            {
                return NotFound();
            }

            if (productModel.CommunityIds == null)
            {
                ModelState.AddModelError("Total", "El total no puede ser cero. Agregue productos válidos.");
            }

            if (productModel.Price <= 0 || productModel.Price > 999999)
            {
                ModelState.AddModelError("Price", "El precio no debe ser negativo, cero o mayor a 999999");
                return View(productModel);
            }

            if (!IsValidProductName(productModel.Name))
            {
                ModelState.AddModelError("Name", "El nombre debe contener solo letras, números y espacios, y tener un máximo de 50 caracteres.");
            }

            if (!IsValidProductPrice(productModel.Price))
            {
                ModelState.AddModelError("Price", "El precio debe estar entre 1 y 999999999.");
            }

            if (!IsValidProductWeight(productModel.WeightKg))
            {
                ModelState.AddModelError("WeightKg", "El peso debe estar entre 1 y 999999.");
            }

            if (!IsValidProductSize(productModel.Dimension))
            {
                ModelState.AddModelError("Dimension", "El formato del tamaño no es válido. Debe ser en el formato 'n1xn2xn3'.");
            }

            if (!IsValidProductStock(productModel.Stock))
            {
                ModelState.AddModelError("Stock", "El stock debe estar entre 1 y 999999999.");
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



        private bool IsValidProductName(string name)
        {
            // Validar que solo contiene letras, números y espacios y tiene un máximo de 50 caracteres
            return !string.IsNullOrEmpty(name) && name.Length <= 50 && System.Text.RegularExpressions.Regex.IsMatch(name, "^[a-zA-Z0-9\\s]+$");
        }

        private bool IsValidProductPrice(double price)
        {
            // Validar que el precio esté entre 1 y 999999999
            return price >= 1 && price <= 999999999;
        }

        private bool IsValidProductWeight(int weight)
        {
            // Validar que el peso esté entre 1 y 999999
            return weight >= 1 && weight <= 999999;
        }

        private bool IsValidProductSize(string size)
        {
            // Validar el formato del tamaño (por ejemplo, "17x20x30")
            return !string.IsNullOrEmpty(size) && System.Text.RegularExpressions.Regex.IsMatch(size, "^[0-9]{1,3}x[0-9]{1,3}x[0-9]{1,3}$");
        }

        private bool IsValidProductStock(int stock)
        {
            // Validar que el stock esté entre 1 y 999999999
            return stock >= 1 && stock <= 999999999;
        }



    }
}
