using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FinalHPMS.Views.Roles.ViewModels;

public class RoleDetailsViewModel
{
    [Display(Name="Nombre de Rol")]
    public required string RoleName{get;set;}
    //[Display(Name="Fecha de Creación")]
    //public DateTime CreationDate { get; set; }
    //[Display(Name="Creado por")]
    //public string CreatedBy { get; set; }
}