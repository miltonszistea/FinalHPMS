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

namespace FinalHPMS.Controllers
{
    public class TicketController : Controller
    {
        private readonly ProductContext _context;

        public TicketController(ProductContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index(string filter)
        {
            var query = from ticket in _context.Product select ticket;
            if(!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.ToLower().Contains(filter) ||
                             x.Price.ToString().Contains(filter));
                
            }

            var model = new ProductViewModel();
            model.Products = await query.ToListAsync();

              return _context.Product != null ? 
                          View(model) :
                          Problem("Entity set 'ProductContext.Product'  is null.");
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.Include(x=>x.Communities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductDetailViewModel();
            viewModel.Name = product.Name;
            viewModel.Dimension = product.Dimension;
            viewModel.Category = product.Category;
            viewModel.WeightKg = product.WeightKg;
            viewModel.ShippingAvailable = product.ShippingAvailable;
            viewModel.Stock = product.Stock;
            viewModel.Price = product.Price;
            viewModel.Communities = product.Communities;


            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, ProductCreateViewModel productCreateViewModel)
        { 
            var CommunitiesList = _context.Community
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            var model = new Product() {
                    Name = productCreateViewModel.Name,
                    Price = productCreateViewModel.Price,
                    Category = productCreateViewModel.Category,
                    WeightKg = productCreateViewModel.WeightKg,
                    ShippingAvailable = productCreateViewModel.ShippingAvailable,
                    Dimension = productCreateViewModel.Dimension,
                    Stock = productCreateViewModel.Stock,  
                    };

            // ModelState.Remove("CommunityId");
            ModelState.Remove("Communities");

            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }            
            return View(model);

        }


        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("Id,Name,Price,Category,WeightKg,ShippingAvailable,Dimension,Stock, Communities, CommunityId")] Product product)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Add(product);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(product);
        // }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Category,WeightKg,ShippingAvailable,Dimension,Stock")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.Product == null)
            {
                return Problem("Entity set 'ProductContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}