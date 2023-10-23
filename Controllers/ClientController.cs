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
    public class ClientController : Controller
    {
        private readonly ProductContext _context;

        public ClientController(ProductContext context)
        {
            _context = context;
        }

        // GET: Client
        public async Task<IActionResult> Index(string Filter)
        {
            var query = from client in _context.Client select client ;
            
            var tickets = query.Include(x=>x.Tickets);

            if (!string.IsNullOrEmpty(Filter))
            {
                Filter = Filter.ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(Filter) ||
                                        x.Apellido.ToLower().Contains(Filter) ||
                                        x.Mail.ToLower().Contains(Filter));
            }

            var model = new ClientViewModel();
            model.Clients = await query.ToListAsync();

            return _context.Client != null ? 
            View(model) :
            Problem("Entity set 'ClientContext.Client'  is null.");
        }

        // GET: Client/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientCreateViewModel clientViewModel)
        {

            var model = new Client(){
                    Name = clientViewModel.Name,
                    Apellido = clientViewModel.Apellido,
                    Address = clientViewModel.Address,
                    Phone = clientViewModel.Phone,
                    Mail = clientViewModel.Mail,
                    };

            ModelState.Remove("TicketId");
            ModelState.Remove("Tickets");
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientViewModel);
        }

        // GET: Client/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Apellido,Address,Phone,Mail")] Client client)
        {

            if (id != client.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Tickets");
            if (ModelState.IsValid)
            {
                //var client = _context.Client.Find(id);
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Client/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'ProductContext.Client'  is null.");
            }
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
          return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
