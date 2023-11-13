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
using Microsoft.AspNetCore.Authorization;
using FinalHPMS.Services;

namespace FinalHPMS.Controllers
{
    [Authorize(Roles = "Administrator,Employee,Supervisor")]
    public class TicketController : Controller
    {
        private ITicketService _ticketService;
        private IClientService _clientService;
        public TicketController(ITicketService ticketService, IClientService clientService)
        {
            _ticketService = ticketService;
            _clientService = clientService;

        }

        // GET: Ticket
        public IActionResult Index(string filter)
        {
            var ticketContext = _ticketService.GetAll(filter);
            return View(ticketContext);
        }

        // GET: Ticket/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticketDetailViewModel = _ticketService.GetDetails(id.Value);

            if (ticketDetailViewModel == null)
            {
                return NotFound();
            }

            return View(ticketDetailViewModel);
        }

        // GET: Ticket/Create
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult Create()
        {
            var clientList = _ticketService.GetAll();
            ViewData["Client"] = new SelectList(clientList.Tickets.ToList()
            .Select(c => new SelectListItem
            {
                Text = c.Client.Name,
                Value = c.Id.ToString()
            }), "Value", "Text");
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult Create(Ticket ticket, TicketCreateViewModel ticketCreateViewModel)
        {
            //ModelState.Remove("Communities");
            if (ModelState.IsValid)
            {
                var model = new Ticket()
                {
                    Id = ticketCreateViewModel.Id,
                    //Client = ticketCreateViewModel.Clients,
                    PaymentMethod = ticketCreateViewModel.PaymentMethod,
                    DateAndHour = ticketCreateViewModel.DateAndHour,
                    Total = ticketCreateViewModel.Total,
                    ProductTickets = ticketCreateViewModel.Products,
                    //Communities = ticketCreateViewModel.Communities,
                };
                _ticketService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            return View(ticketCreateViewModel);

        }


        // GET: Ticket/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _ticketService.GetDetails(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id, [Bind("Id,Name,Price,Category,WeightKg,ShippingAvailable,Dimension,Stock")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Communities");
            if (ModelState.IsValid)
            {
                _ticketService.Update(ticket, id);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Ticket/Delete/5
        [Authorize(Roles = "Administrator,Supervisor")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _ticketService.GetDetails(id.Value);
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
            var ticket = _ticketService.GetTicket(id);
            if (ticket != null)
            {
                _ticketService.Delete(ticket);
            }
            return RedirectToAction(nameof(Index));
        }

        // private bool TickettExists(int id)
        // {
        //   return _ticketService.GetTicket(id) != null;
        // }
    }
}