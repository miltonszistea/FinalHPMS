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
        if(productCreate.Stock < 1)
        {
            return;
        }
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

    public void Update(Product product, List<int> CommunityIds)
    {
        if(product.Stock < 1)
        {
            return;
        }
        var existingProduct =  _context.Products
            .Include(p => p.ProductCommunities)
            .FirstOrDefault(p => p.Id == product.Id);

        if (existingProduct == null)
        {
            return;
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.ProductCategory = product.ProductCategory;
        existingProduct.WeightKg = product.WeightKg;
        existingProduct.ShippingAvailable = product.ShippingAvailable;
        existingProduct.Dimension = product.Dimension;
        existingProduct.Stock = product.Stock;
        existingProduct.ProductCommunities = CommunityIds
            .Select(communityId => new ProductCommunity
            {
                ProductId = product.Id,
                CommunityId = communityId
            })
            .ToList();
        //_context.Update(product);
        _context.SaveChanges();
    }

    
    public List<Product> GetProductsByCommunityId(int id)
    {   
        var productsInCommunity = (
            from product in _context.Products
            join productCommunity in _context.ProductCommunity
            on product.Id equals productCommunity.ProductId
            where productCommunity.CommunityId == id
            select product
            ).ToList();

        return productsInCommunity;
    }

    public  List<Product> GetProductsByTicketId(int ticketId)
 {
     var productsInTicket =  (
         from product in _context.Products
         join productTicket in _context.ProductTicket
         on product.Id equals productTicket.ProductId
         where productTicket.TicketId == ticketId
         select product
         ).ToList();


     return productsInTicket;
 }
}
