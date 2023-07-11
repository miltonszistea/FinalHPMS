using FinalHPMS.Models;
namespace FinalHPMS.ViewModels;

public class ProductDetailViewModel{
    public  string Name { get; set; }
    public int Price { get; set; }
    public ProductCategory Category { get; set; }
    public int WeightKg { get; set; }
    public bool ShippingAvailable { get; set; }
    public string Dimension { get; set; }
    public int Stock { get; set; }
    public List<Community> Communities {get;set;}
}