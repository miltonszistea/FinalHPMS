using System.ComponentModel.DataAnnotations;

namespace FinalHPMS.Views.Roles.ViewModels;

public class RoleCreateViewModel
{
    [Display(Name="Nombre de Rol")]
    public string RoleName{get;set;}
}