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
      var CommunitiesList = _context.Communities
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
        _context.Communities.Remove(community);
        _context.SaveChanges();
    }

    public Community? GetDetails(int id)
    {
        var community = _context.Communities
        .Include(x=>x.ProductCommunities)
        .Include(x=>x.Tickets)
        .FirstOrDefault(m => m.Id == id);

        return community;
    }

public CommunityViewModel GetAll(string filter)
    {
        var query = from community in _context.Communities select community;

        //var communities = query.Include(x=>x.Products);

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

public CommunityViewModel GetAll()
{
    var query = from community in _context.Communities select community;
    //var communities = query.Include(x=>x.ProductCommunities);
    var model = new CommunityViewModel();
    model.Communities = query.ToList();
    return model;
}

public Community? GetCommunity(int id)
    {
        var community = _context.Communities
        //.Include(x=>x.Products)
        .FirstOrDefault(m => m.Id == id);
        return community;
    }

    public void Update(Community community, int id)
    {      
        _context.Update(community);
        _context.SaveChanges();           
    }   
}
