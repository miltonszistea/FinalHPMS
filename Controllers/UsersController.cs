using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalHPMS.Models;
using FinalHPMS.Services;
using Microsoft.AspNetCore.Identity;
using FinalHPMS.Views.Users.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalHPMS.Controllers;

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
        var userEditViewModel = new UserEditViewModel();
        userEditViewModel.UserName = user.UserName;
        userEditViewModel.Email = user.Email;
        userEditViewModel.Roles = new SelectList(_rolesManager.Roles.ToList());
        return View(userEditViewModel);
    }

    //update
    //delete

}
