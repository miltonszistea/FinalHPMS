using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalHPMS.Views.Roles.ViewModels;

public class RoleEditViewModel
{
    [Display(Name="Nombre de Rol")]
    public required string RoleName{get;set;}

    public string CurrentRoleName { get; set; }
}