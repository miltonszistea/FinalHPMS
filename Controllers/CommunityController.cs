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
    public class CommunityController : Controller
    {
        private ICommunityService _communityService;
        public CommunityController(ICommunityService communityService)
        {
            _communityService = communityService;
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

        // private bool CommunityExists(int id)
        // {
        //   return _communityService.GetCommunity(id) != null;
        // }
    }
}
