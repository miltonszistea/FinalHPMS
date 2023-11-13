using System.ComponentModel.DataAnnotations;

namespace FinalHPMS.Models;

public class Product
{

    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public double Price { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public int WeightKg { get; set; }
    public bool ShippingAvailable { get; set; }
    public required string Dimension { get; set; }
    [Required]
    public int Stock { get; set; }
    public int Quantity { get; set; }
    public List<ProductCommunity>? ProductCommunities { get; set; } 
    public List<ProductTicket>? ProductTickets { get; set; }

}