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
        var users = _userManager.Users.Select(user => new UserIndexViewModel
        {
            UserName = user.UserName,
            Email = user.Email,
            Role = _userManager.GetRolesAsync(user).Result.FirstOrDefault(),
            Id = user.Id
        }).ToList();
        return View(users);
    }

    //GET Edit
    public async Task<IActionResult> Edit(string userid)
    {
        var user = await _userManager.FindByIdAsync(userid);
        var userEditViewModel = new UserEditViewModel
        {
            UserName = user.UserName,
            Email = user.Email,
            Roles = new SelectList(_rolesManager.Roles.ToList())
        };
        return View(userEditViewModel);
    }

    //POST Edit
    [HttpPost]
    public async Task<IActionResult> Edit(UserEditViewModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, model.Role);
            RedirectToAction("Index");
        }
        return RedirectToAction("Index");
    }
    //GET Details
        public async Task<IActionResult> Details(string userid)
        {
            if (userid == null)
            {
                return NotFound();
            }
        var user = await _userManager.FindByIdAsync(userid);
        var Rol = await _userManager.GetRolesAsync(user);
        var userDetailViewModel = new UserDetailViewModel
        {
            UserName = user.UserName,
            Email = user.Email,
            Role = Rol[0],
        };

            if (userDetailViewModel == null)
            {
                return NotFound();
            }

            return View(userDetailViewModel);
        }
    
    // GET: User/Delete/5
    public async Task<IActionResult> Delete(string userid)
    {
        if (userid == null)
        {
            return NotFound();
        }

        var user = await _userManager.FindByIdAsync(userid);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }
}
