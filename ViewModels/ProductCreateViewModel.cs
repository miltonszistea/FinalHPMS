using FinalHPMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FinalHPMS.ViewModels;
using System.ComponentModel.DataAnnotations;

public class ProductCreateViewModel{
    
    [Display(Name="Nombre")]
    public required string Name { get; set; }
    [Display(Name="Precio")]
    public int Price { get; set; }
    [Display(Name="Categoría")]
    public ProductCategory Category { get; set; }
    [Display(Name="Peso")]
    public int WeightKg { get; set; }
    [Display(Name="¿Envío disponible?")]
    public bool ShippingAvailable { get; set; }
    [Display(Name="Tamaño")]
    public required string Dimension { get; set; }
    public int Stock { get; set; }
    [Display(Name="Comunidad")]
    public int? CommunityId {get;set;}
    [Display(Name="Comunidad")]
    public List<SelectListItem>? Communities {get;set;}
}