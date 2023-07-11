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
    public class CommunityController : Controller
    {
        private readonly ProductContext _context;

        public CommunityController(ProductContext context)
        {
            _context = context;
        }

        // GET: Community
        public async Task<IActionResult> Index(string filtercommunity)
        {
            var query = from community in _context.Community select community ;
            if(!string.IsNullOrEmpty(filtercommunity))
            {
                query = query.Where(x => x.Name.Contains(filtercommunity) ||
                             x.CityAndCountry.ToString().Contains(filtercommunity)
                             ||x.Mail.ToString().Contains(filtercommunity));
                
            }
            var products = query.Include(p=>p.Products).Select(p=>p.Products).ToList();
            var model = new CommunityViewModel();
            model.Communities = await query.ToListAsync();

                return _context.Community != null ? 
                View(model) :
                Problem("Entity set 'ProductContext.Product'  is null.");
        }

        // GET: Community/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Community == null)
            {
                return NotFound();
            }

            var community = await _context.Community
                .FirstOrDefaultAsync(m => m.Id == id);
            if (community == null)
            {
                return NotFound();
            }

            return View(community);
        }

        // GET: Community/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Dimension");
            return View();
        }

        // POST: Community/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommunityCreateViewModel community)
        {

            var model = new CommunityCreateViewModel() {
                    Name = community.Name,
                    CityAndCountry = community.CityAndCountry,
                    Address = community.Address,
                    Phone = community.Phone,
                    Mail = community.Mail,
                    CommunityType = community.CommunityType,
                    //Products = new List<SelectListItem>(),
                    ProductsIds = new List<int>()
                    };



            if (ModelState.IsValid)
            {
                _context.Add(community);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Dimension", community.ProductsIds);
            return View(community);
        }

        // GET: Community/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Community == null)
            {
                return NotFound();
            }

            var community = await _context.Community.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Dimension", community.ProductId);
            return View(community);
        }

        // POST: Community/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CityAndCountry,Address,Phone,Mail,CommunityType,ProductId")] Community community)
        {
            if (id != community.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(community);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunityExists(community.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Dimension", community.ProductId);
            return View(community);
        }

        // GET: Community/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Community == null)
            {
                return NotFound();
            }

            var community = await _context.Community
                .FirstOrDefaultAsync(m => m.Id == id);
            if (community == null)
            {
                return NotFound();
            }

            return View(community);
        }

        // POST: Community/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Community == null)
            {
                return Problem("Entity set 'ProductContext.Community'  is null.");
            }
            var community = await _context.Community.FindAsync(id);
            if (community != null)
            {
                _context.Community.Remove(community);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommunityExists(int id)
        {
          return (_context.Community?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
