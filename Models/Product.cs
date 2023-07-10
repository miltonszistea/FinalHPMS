
using System.ComponentModel.DataAnnotations;

namespace FinalHPMS.Models;

public class Product{
    
    [Required]
    public int Id { get; set; }

    [Display(Name="Nombre")]
    [Required]
    public required string Name { get; set; }
    
    [Display(Name="Precio")]
    [Required]
    public decimal Price { get; set; }
    
    [Display(Name="Categoría")]
    [Required]
    public ProductCategory Category { get; set; }
    
    [Display(Name="Peso")]
    [Required]
    public decimal WeightKg { get; set; }

    [Display(Name="Envío disponible")]
    [Required]
    public bool ShippingAvailable { get; set; }
}