using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalHPMS.Views.Users.ViewModels;

public class UserDetailViewModel
{
    [Display(Name="Nombre")]
    public string UserName{get;set;}
    [Display(Name="Correo")]
    public string Email{get;set;}
    [Display(Name="Rol")]
    public string Role{get;set;}
    public SelectList Roles {get;set;}
}