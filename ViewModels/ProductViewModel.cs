using FinalHPMS.Models;
namespace FinalHPMS.ViewModels;

public class ProductViewModel{
    public List<Product> Products{get;set;} = new List<Product>();
    public string? Filter {get; set;}
}