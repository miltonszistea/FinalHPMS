using FinalHPMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FinalHPMS.ViewModels;

public class ProductCreateViewModel{
    public  string Name { get; set; }
    public int Price { get; set; }
    public ProductCategory Category { get; set; }
    public int WeightKg { get; set; }
    public bool ShippingAvailable { get; set; }
    public string Dimension { get; set; }
    public int Stock { get; set; }
    public int? CommunityId {get;set;}
    public List<SelectListItem>? Communities {get;set;}
}