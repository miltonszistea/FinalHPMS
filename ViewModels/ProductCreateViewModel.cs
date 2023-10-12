using FinalHPMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FinalHPMS.ViewModels;
using System.ComponentModel.DataAnnotations;

public class ProductCreateViewModel{
    
    public required string Name { get; set; }
    public int Price { get; set; }
    public ProductCategory Category { get; set; }
    public int WeightKg { get; set; }
    public bool ShippingAvailable { get; set; }
    public required string Dimension { get; set; }
    public int Stock { get; set; }
    public int? CommunityId {get;set;}
    public List<SelectListItem>? Communities {get;set;}
}