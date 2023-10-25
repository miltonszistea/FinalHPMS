using FinalHPMS.Data;
using FinalHPMS.Models;
using FinalHPMS.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalHPMS.Services;

public class CommunityService : ICommunityService
{
    private readonly ProductContext _context;
    public CommunityService(ProductContext context)
    {
        _context = context;
    }
    public void Create(Community communityCreate)
    {
      var CommunitiesList = _context.Community
        .Select(c => new SelectListItem
        {
         Value = c.Id.ToString(),
         Text = c.Name
        }).ToListAsync();

        _context.Add(communityCreate);
        _context.SaveChanges();
    }

    public void Delete(Community community)
    {
        _context.Community.Remove(community);
        _context.SaveChanges();
    }

    public Community? GetDetails(int id)
    {
        var community = _context.Community
        .Include(x=>x.Products)
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
        return community;
    }

public CommunityViewModel GetAll(string filter)
    {
        var query = from community in _context.Community select community;

        var communities = query.Include(x=>x.Products);

        if(!string.IsNullOrEmpty(filter))
            {
            query = query.Where(x => x.Name.ToLower().Contains(filter.ToLower()) ||
                             x.CityAndCountry.ToLower().Contains(filter.ToLower())
                             ||x.Mail.ToLower().Contains(filter.ToLower()));    
                
            }

            var model = new CommunityViewModel();
            model.Communities = query.ToList();
            return model;
    }

public Community? GetCommunity(int id)
    {
        var community = _context.Community
        .Include(x=>x.Products)
        .FirstOrDefault(m => m.Id == id);
        return community;
    }

    public void Update(Community community, int id)
    {      
        _context.Update(community);
        _context.SaveChanges();           
    }

}