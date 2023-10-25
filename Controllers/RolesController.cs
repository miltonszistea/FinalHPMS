using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalHPMS.Models;
using FinalHPMS.Services;
using Microsoft.AspNetCore.Identity;

namespace FinalHPMS.Controllers;

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

    //edit
    //update
    //delete

}
