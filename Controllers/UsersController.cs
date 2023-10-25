using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalHPMS.Models;
using FinalHPMS.Services;
using Microsoft.AspNetCore.Identity;
using FinalHPMS.Views.Users.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace FinalHPMS.Controllers;

//[Authorize(Roles = "Administrator")]
public class UsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _rolesManager;
    public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> rolesManager)
    {
        _userManager = userManager;
        _rolesManager = rolesManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }

    //GET Edit
    public async Task<IActionResult> Edit(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var userEditViewModel = new UserEditViewModel
        {
            UserName = user.UserName,
            Email = user.Email,
            Roles = new SelectList(_rolesManager.Roles.ToList())
        };
        return View(userEditViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserEditViewModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user != null)
        {
            RedirectToAction("Index");
            await _userManager.AddToRoleAsync(user, model.Role);
        }
        return RedirectToAction("Index");
    }
    //update
    //delete

}
