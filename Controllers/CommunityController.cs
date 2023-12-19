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
    public class CommunityController : Controller
    {
        private ICommunityService _communityService;
        private IProductService _productService;

        private ITicketService _ticketService;
        public CommunityController(ICommunityService communityService, IProductService productService, ITicketService ticketService)
        {
            _communityService = communityService;
            _productService = productService;
            _ticketService = ticketService;
        }

        // GET: Community
        public IActionResult Index(string filter)
        {
            var productContext = _communityService.GetAll(filter);

            return View(productContext);
        }

        // GET: Community/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var communityDetailViewModel = _communityService.GetDetails(id.Value);

            if (communityDetailViewModel == null)
            {
                return NotFound();
            }

            return View(communityDetailViewModel);
        }
        public async Task<IActionResult> Products(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var communityDetailViewModel = _communityService.GetDetails(id);
            var model = new CommunityCreateViewModel(){
                Products = _productService.GetProductsByCommunityId(id),
                Name = communityDetailViewModel.Name
            };

            if (communityDetailViewModel == null)
            {
                return NotFound();
            }

            return View(model);
        }



        // GET: Community/Create
        public IActionResult Create()
        {
            //ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Dimension");
            return View();
        }

        // POST: Community/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Community community, CommunityCreateViewModel communityViewModel)
        {
            ModelState.Remove("Tickets");
            ModelState.Remove("Products");
            ModelState.Remove("ProductId");
            ModelState.Remove("TicketId");

        if (!IsValidBranchCommunity(communityViewModel.Name))
        {
            ModelState.AddModelError("Name", "El nombre de la comunidad debe contener solo letras y tener un máximo de 50 caracteres.");
        }

        if (!IsValidBranchCityCountry(communityViewModel.CityAndCountry))
        {
            ModelState.AddModelError("CityAndCountry", "La ciudad y país deben contener solo letras y espacios, y tener un máximo de 50 caracteres.");
        }

        if (!IsValidBranchAddress(communityViewModel.Address))
        {
            ModelState.AddModelError("Address", "El formato de la dirección no es válido. Debe contener solo letras, números y espacios, y tener un máximo de 40 caracteres.");
        }

        if (!IsValidBranchPhone(communityViewModel.Phone))
        {
            ModelState.AddModelError("Phone", "El número de teléfono debe contener solo números y tener un máximo de 20 caracteres.");
        }

        if (!IsValidBranchEmail(communityViewModel.Mail))
        {
            ModelState.AddModelError("Mail", "El formato del correo electrónico no es válido.");
        }
            if (ModelState.IsValid)
            {
                var model = new Community()
                {
                    Name = communityViewModel.Name,
                    CityAndCountry = communityViewModel.CityAndCountry,
                    Address = communityViewModel.Address,
                    Phone = communityViewModel.Phone,
                    Mail = communityViewModel.Mail,
                    CommunityType = communityViewModel.CommunityType,
                };
                _communityService.Create(model);
                return RedirectToAction(nameof(Index));
            }
            return View(communityViewModel);
        }

        // GET: Community/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = _communityService.GetDetails(id.Value);
            if (community == null)
            {
                return NotFound();
            }
            return View(community);
        }

        // POST: Community/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id, [Bind("Id,Name,CityAndCountry,Address,Phone,Mail,CommunityType,ProductId")] Community community)
        {
            if (id != community.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Communities");

            if (!IsValidBranchCommunity(community.Name))
            {
                ModelState.AddModelError("Name", "El nombre de la comunidad debe contener solo letras y tener un máximo de 50 caracteres.");
            }

            if (!IsValidBranchCityCountry(community.CityAndCountry))
            {
                ModelState.AddModelError("CityAndCountry", "La ciudad y país deben contener solo letras y espacios, y tener un máximo de 50 caracteres.");
            }

            if (!IsValidBranchAddress(community.Address))
            {
                ModelState.AddModelError("Address", "El formato de la dirección no es válido. Debe contener solo letras, números y espacios, y tener un máximo de 40 caracteres.");
            }

            if (!IsValidBranchPhone(community.Phone))
            {
                ModelState.AddModelError("Phone", "El número de teléfono debe contener solo números y tener un máximo de 20 caracteres.");
            }

            if (!IsValidBranchEmail(community.Mail))
            {
                ModelState.AddModelError("Mail", "El formato del correo electrónico no es válido.");
            }

            if (ModelState.IsValid)
            {
                _communityService.Update(community, id);
                return RedirectToAction(nameof(Index));
            }
            return View(community);
        }

        // GET: Community/Delete/5
        [Authorize(Roles = "Administrator,Supervisor")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = _communityService.GetDetails(id.Value);
            if (community == null)
            {
                return NotFound();
            }

            return View(community);
        }

        // POST: Community/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Supervisor")]
        public IActionResult DeleteConfirmed(int id)
        {
            var community = _communityService.GetCommunity(id);
            if (community != null)
            {
                _communityService.Delete(community);
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Tickets(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var communityDetailViewModel = _communityService.GetDetails(id);
            var model = new CommunityCreateViewModel(){
                Products = _productService.GetProductsByCommunityId(id),
                Name = communityDetailViewModel.Name,
                Tickets = _ticketService.GetTicketsByCommunityId(id)
                
            };

            if (communityDetailViewModel == null)
            {
                return NotFound();
            }

            return View(model);
        }

        private bool IsValidBranchCommunity(string community)
        {
            // Validar que solo contiene letras y tiene un máximo de 50 caracteres
            return !string.IsNullOrEmpty(community) && community.Length <= 50 && community.All(char.IsLetter);
        }
        private bool IsValidBranchCityCountry(string cityCountry)
        {
            // Validar que solo contiene letras y espacios, y tiene un máximo de 50 caracteres
            return !string.IsNullOrEmpty(cityCountry) && cityCountry.Length <= 50 && cityCountry.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }
        private bool IsValidBranchAddress(string address)
        {
            // Validar que la dirección tenga solo letras, números y espacios, y tiene un máximo de 40 caracteres
            return !string.IsNullOrEmpty(address) && address.Length <= 40 && address.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c));
        }
        private bool IsValidBranchPhone(string phone)
        {
            // Validar que el teléfono tenga solo números y tiene un máximo de 20 caracteres
            return !string.IsNullOrEmpty(phone) && phone.Length <= 20 && phone.All(char.IsDigit);
        }
        private bool IsValidBranchEmail(string email)
        {
            // Validar el formato de correo electrónico
            return !string.IsNullOrEmpty(email) && new EmailAddressAttribute().IsValid(email);
        }
    }
}
