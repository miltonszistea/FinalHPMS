using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalHPMS.Models;
using FinalHPMS.Services;
using Microsoft.AspNetCore.Identity;

namespace FinalHPMS.Controllers;

public class UsersController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    public UsersController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }

    //edit
    //update
    //delete

}
