using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalHPMS.Models;
using FinalHPMS.Services;
using Microsoft.AspNetCore.Identity;
using FinalHPMS.Views.Roles.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FinalHPMS.Controllers;

//[Authorize(Roles = "Administrator")]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _rolesManager;
    public RolesController(RoleManager<IdentityRole> rolesManager)
    {
        _rolesManager = rolesManager;
    }

    public IActionResult Index()
    {
        var roles = _rolesManager.Roles.ToList();
        return View(roles);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(RoleCreateViewModel model)
    {
        if(string.IsNullOrEmpty(model.RoleName))
        {
              return View();  
        }

        var role = new IdentityRole(model.RoleName);
        _rolesManager.CreateAsync(role);

        return RedirectToAction("Index");
    }
    //edit
    //update
    //delete

}
