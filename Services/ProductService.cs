using FinalHPMS.Data;
using FinalHPMS.Models;
using FinalHPMS.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalHPMS.Services;

public class ProductService : IProductService
{
    private readonly ProductContext _context;
    public ProductService(ProductContext context)
    {
        _context = context;
    }
    public void Create(Product productCreate)
    {
      var CommunitiesList = _context.Community
        .Select(c => new SelectListItem
        {
         Value = c.Id.ToString(),
         Text = c.Name
        }).ToListAsync();

        _context.Add(productCreate);
        _context.SaveChanges();
    }

    public void Delete(Product product)
    {
        _context.Product.Remove(product);
        _context.SaveChanges();
    }

    public Product? GetDetails(int id)
    {
        var product = _context.Product
        .Include(x=>x.Communities)
        .FirstOrDefault(m => m.Id == id);

        // var productDetailviewModel = new ProductDetailViewModel
        // {
        //     Name = product.Name,
        //     Dimension = product.Dimension,
        //     Category = product.Category,
        //     WeightKg = product.WeightKg,
        //     ShippingAvailable = product.ShippingAvailable,
        //     Stock = product.Stock,
        //     Price = product.Price,
        //     Communities = product.Communities
        // };
        return product;
    }

    public ProductViewModel GetAll(string filter)
    {
        var query = from product in _context.Product select product;

        var communities = query.Include(x=>x.Communities);

        if(!string.IsNullOrEmpty(filter))
            {
                query = query.Where(x => x.Name.ToLower().Contains(filter) ||
                             x.Price.ToString().Contains(filter));
                
            }

            var model = new ProductViewModel();
            model.Products = query.ToList();
            return model;
    }

public Product? GetProduct(int id)
    {
        var product = _context.Product
        .Include(x=>x.Communities)
        .FirstOrDefault(m => m.Id == id);
        return product;
    }

    public void Update(Product product, int id)
    {      
        _context.Update(product);
        _context.SaveChanges();           
    }
}
