using FinalHPMS.Models;
namespace FinalHPMS.ViewModels;
using System.ComponentModel.DataAnnotations;
public class ProductDetailViewModel{
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
    [Display(Name="Comunidades")]
    public  List<Community> Communities {get;set;}
    public List<Ticket> Tickets {get; set;}
}