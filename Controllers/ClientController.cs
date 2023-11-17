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
    public class ClientController : Controller
    {
        private IClientService _clientService;
        private IProductService _productService;
        private ITicketService _ticketService;
        public ClientController(IClientService clientService, IProductService productService, ITicketService ticketService)
        {
            _clientService = clientService;
            _productService = productService;
            _ticketService = ticketService;
        }

        // GET: Client
        public IActionResult Index(string filter)
        {
            var productContext = _clientService.GetAll(filter);
            return View(productContext);
        }

        // GET: Client/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clientDetailViewModel = _clientService.GetDetails(id.Value);

            if (clientDetailViewModel == null)
            {
                return NotFound();
            }
            return View(clientDetailViewModel);
        }

        // GET: Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClientCreateViewModel clientViewModel)
        {
            ModelState.Remove("TicketId");
            ModelState.Remove("Tickets");
            ModelState.Remove("DateAndHour");
            if (ModelState.IsValid)
            {
                var model = new Client()
                {
                    Name = clientViewModel.Name,
                    Apellido = clientViewModel.Apellido,
                    Address = clientViewModel.Address,
                    Phone = clientViewModel.Phone,
                    Mail = clientViewModel.Mail,
                };
                _clientService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            return View(clientViewModel);
        }

        // GET: Client/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _clientService.GetDetails(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id, [Bind("Id,Name,Apellido,Address,Phone,Mail")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Communities");
            if (ModelState.IsValid)
            {
                _clientService.Update(client, id);
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Client/Delete/5
        [Authorize(Roles = "Administrator,Supervisor")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _clientService.GetDetails(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Supervisor")]
        public IActionResult DeleteConfirmed(int id)
        {
            var client = _clientService.GetClient(id);
            if (client != null)
            {
                _clientService.Delete(client);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Tickets(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientTicketViewModel = _clientService.GetDetails(id);

            if (clientTicketViewModel == null)
            {
                return NotFound();
            }

            var model = new ClientTicketViewModel
            {
                Name = clientTicketViewModel.Name,
                Tickets = _ticketService.GetTicketsByClientId(id)
            };

            return View(model);
        }
    }
}
