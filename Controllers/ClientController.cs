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
using System.ComponentModel.DataAnnotations;

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

            // Validaciones personalizadas
            if (!IsValidName(clientViewModel.Name))
            {
                ModelState.AddModelError("Name", "El nombre debe contener solo letras y tener un máximo de 25 caracteres.");
            }

            if (!IsValidName(clientViewModel.Apellido))
            {
                ModelState.AddModelError("Apellido", "El apellido debe contener solo letras y tener un máximo de 25 caracteres.");
            }

            if (!IsValidEmail(clientViewModel.Mail))
            {
                ModelState.AddModelError("Mail", "El formato del correo electrónico no es válido.");
            }

            if (!IsValidPhone(clientViewModel.Phone))
            {
                ModelState.AddModelError("Phone", "El número de teléfono debe contener solo números y tener un máximo de 20 caracteres.");
            }


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

                if (!IsValidName(client.Name))
                {
                    ModelState.AddModelError("Name", "El nombre debe contener solo letras y tener un máximo de 25 caracteres.");
                }

                if (!IsValidName(client.Apellido))
                {
                    ModelState.AddModelError("Apellido", "El apellido debe contener solo letras y tener un máximo de 25 caracteres.");
                }

                if (!IsValidEmail(client.Mail))
                {
                    ModelState.AddModelError("Mail", "El formato del correo electrónico no es válido.");
                }

                if (!IsValidPhone(client.Phone))
                {
                    ModelState.AddModelError("Phone", "El número de teléfono debe contener solo números y tener un máximo de 20 caracteres.");
                }

                if (!IsValidAddress(client.Address))
                {
                    ModelState.AddModelError("Address", "La dirección debe contener solo letras, números y espacios, y tener un máximo de 40 caracteres.");
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





        // Funciones de validación personalizadas
        private bool IsValidName(string name)
        {
            // Validar que solo contiene letras y tiene un máximo de 25 caracteres
            return !string.IsNullOrEmpty(name) && name.Length <= 25 && name.All(char.IsLetter);
        }

        private bool IsValidEmail(string email)
        {
            // Validar el formato de correo electrónico
            return !string.IsNullOrEmpty(email) && new EmailAddressAttribute().IsValid(email);
        }

        private bool IsValidPhone(string phone)
        {
            // Validar que solo contiene números y tiene un máximo de 20 caracteres
            return !string.IsNullOrEmpty(phone) && phone.Length <= 20 && phone.All(char.IsDigit);
        }

        private bool IsValidAddress(string address)
        {
            // Validar que solo contiene letras, números y espacios y tiene un máximo de 40 caracteres
            return !string.IsNullOrEmpty(address) && address.Length <= 40 && System.Text.RegularExpressions.Regex.IsMatch(address, "^[a-zA-Z0-9\\s]+$");
        }

    }
}
