
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FinalHPMS.Models;

public class Product{
    
    [Required]
    public int Id { get; set; }

    [Display(Name="Nombre")]
    [Required]
    public required string Name { get; set; }
    
    [Display(Name="Precio")]
    [Required]
    public int Price { get; set; }
    
    [Display(Name="Categoría")]
    [Required]
    public ProductCategory Category { get; set; }
    
    [Display(Name="Peso")]
    [Required]
    public int WeightKg { get; set; }

    [Display(Name="Envío disponible")]
    [Required]
    public bool ShippingAvailable { get; set; }

    [Display(Name="Dimensiones")]
    [Required]
    public required string Dimension { get; set; }

    [Display(Name="Stock Actual")]
    [Required]
    public int Stock { get; set; }

    public int? CommunityId{get;set;}
    public virtual List<Community>? Communities {get;set;} 
}