using System.ComponentModel.DataAnnotations;

namespace FinalHPMS.Models;

public class Product
{

    [Key]
    public int Id { get; set; }
    [Display(Name="Nombre")]
    public required string Name { get; set; }
    [Display(Name="Precio")]
    public double Price { get; set; }
    [Display(Name="Categoría")]
    public ProductCategory ProductCategory { get; set; }
    [Display(Name="Peso")]
    public int WeightKg { get; set; }
    [Display(Name="Envío Disponible")]
    public bool ShippingAvailable { get; set; }
    [Display(Name="Tamaño")]
    public required string Dimension { get; set; }
    [Required]
    public int Stock { get; set; }
    [Display(Name="Cantidad")]
    public int Quantity { get; set; }
    [Display(Name="Comunidades")]
    public List<ProductCommunity>? ProductCommunities { get; set; } 
    public List<ProductTicket>? ProductTickets { get; set; }

}