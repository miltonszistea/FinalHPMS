using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalHPMS.Models;
using FinalHPMS.Services;
using Microsoft.AspNetCore.Identity;
using FinalHPMS.Views.Roles.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FinalHPMS.Controllers;

[Authorize(Roles = "Administrator")]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _rolesManager;
    private readonly UserManager<IdentityUser> _userManager;
    public RolesController(RoleManager<IdentityRole> rolesManager, UserManager<IdentityUser> userManager)
    {
        _rolesManager = rolesManager;
        _userManager = userManager;
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
    if (string.IsNullOrEmpty(model.RoleName) || !IsValidRoleName(model.RoleName))
    {
        ModelState.AddModelError("RoleName", "El nombre del rol debe contener solo letras y tener un máximo de 25 caracteres.");
        return View();
    }

        var role = new IdentityRole(model.RoleName);
        _rolesManager.CreateAsync(role);

        return RedirectToAction("Index");
    }


//GET Edit
public async Task<IActionResult> Edit(string rolid)
{
        var rol = await _rolesManager.FindByIdAsync(rolid);
        if (rol == null)
        {
            return NotFound();
        }

        var userRoleViewModel = new RoleEditViewModel
        {
            RoleName = rol.Name,
        };

        return View(userRoleViewModel);
    }


// POST Edit
[HttpPost]
public async Task<IActionResult> Edit(RoleEditViewModel model)
{
    if (string.IsNullOrEmpty(model.RoleName) || !IsValidRoleName(model.RoleName))
    {
        ModelState.AddModelError("RoleName", "El nombre del rol debe contener solo letras y tener un máximo de 25 caracteres.");
        return RedirectToAction("Index");
    }

    var role = await _rolesManager.FindByNameAsync(model.CurrentRoleName);

    if (role != null)
    {
        var existingRole = await _rolesManager.FindByNameAsync(model.RoleName);

        if (existingRole != null && existingRole.Id != role.Id)
        {
            // Ya existe un rol con el nuevo nombre
            ModelState.AddModelError("RoleName", "Ya existe un rol con el nuevo nombre.");
            return RedirectToAction("Index");
        }

        // Actualizar el nombre del rol
        role.Name = model.RoleName;
        await _rolesManager.UpdateAsync(role);

        return RedirectToAction("Index");
    }

    return RedirectToAction("Index");
}

    
    //GET Details
    public async Task<IActionResult> Details(string rolid)
    {
    var role = await _rolesManager.FindByIdAsync(rolid);

    if (role == null)
    {
        return NotFound();
    }

    var roleDetailsViewModel = new RoleDetailsViewModel
    {
        RoleName = role.Name,
    };

    return View(roleDetailsViewModel);
    }
 
    // GET: User/Delete/5
    public async Task<IActionResult> Delete(string rolid)
    {
        if (rolid == null)
        {
            return NotFound();
        }

        var rol = await _rolesManager.FindByIdAsync(rolid);

            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string rolid)
        {
            var rol = await _rolesManager.FindByIdAsync(rolid);
            if (rol != null)
            {
                await _rolesManager.DeleteAsync(rol);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool IsValidRoleName(string roleName)
        {
            // Validar que solo contiene letras y tiene un máximo de 25 caracteres
            return !string.IsNullOrEmpty(roleName) && roleName.Length <= 25 && roleName.All(char.IsLetter);
        }

}

