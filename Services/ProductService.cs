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
    public void Create(Product productCreate, List<int> CommunityIds)
    {

        //var CommunitiesList = _context.Communities
        //  .Select(c => new SelectListItem
        //  {
        //   Value = c.Id.ToString(),
        //   Text = c.Name
        //  }).ToListAsync();

        _context.Add(productCreate);
        _context.SaveChanges();
        foreach (var CommunityId in CommunityIds)
        {
            var ProductCommunity = new ProductCommunity
            {
                CommunityId = CommunityId,
                ProductId = productCreate.Id
            };
            _context.Add(ProductCommunity);
        }
        _context.SaveChanges();
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public Product? GetDetails(int id)
    {
        var product = _context.Products
        .Include(x => x.ProductCommunities)
        .FirstOrDefault(m => m.Id == id);

        return product;
    }

    public ProductViewModel GetAll(string filter)
    {
        //var query = from product in _context.Products select product;
        //var communities = query.Include(x => x.ProductCommunities);
        IQueryable<Product> query = _context.Products
            .Include(x => x.ProductCommunities)
            .ThenInclude(p => p.Community);

        if (!string.IsNullOrEmpty(filter))
        {
            filter = filter.ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(filter) ||
                         x.Price.ToString().Contains(filter));
        }

        var model = new ProductViewModel();
        model.Products = query.ToList();
        return model;
    }

    public Product? GetProduct(int id)
    {
        var product = _context.Products
        .Include(x => x.ProductCommunities)
        .FirstOrDefault(m => m.Id == id);
        return product;
    }

    public void Update(Product product, int id)
    {
        _context.Update(product);
        _context.SaveChanges();
    }
}
