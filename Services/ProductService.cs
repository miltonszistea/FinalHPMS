using FinalHPMS.Models;

namespace FinalHPMS.Services;

public static class ProductService{
    static List<Product> Products { get; set;}

    static ProductService(){
        Products = new List<Product>
        {
            new Product { Name = "Shredder Pro", 
            Id=1, 
            Category=ProductCategory.Machine, 
            Price=3000,
            WeightKg = 900,
            ShippingAvailable = true,
            Dimension="70x30x40"},
        };
    }

    public static List<Product> GetAll() => Products;

    public static void Add(Product obj){
       if(obj == null){
         return;
       }

       Products.Add(obj);
    }

    public static void Delete(string code){
        var productToDelete = Get(code);

        if (productToDelete != null){
            Products.Remove(productToDelete);
        }
    }
    public static Product? Get(string code) => Products.FirstOrDefault(x => x.Id.ToString().ToLower() == code.ToLower());
}