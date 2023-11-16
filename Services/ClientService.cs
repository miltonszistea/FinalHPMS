using FinalHPMS.Data;
using FinalHPMS.Models;
using FinalHPMS.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalHPMS.Services;

public class ClientService : IClientService
{
    private readonly ProductContext _context;
    public ClientService(ProductContext context)
    {
        _context = context;
    }
    public void Create(Client clientCreate)
    {
      var ClientsList = _context.Clients
        .Select(c => new SelectListItem
        {
         Value = c.Id.ToString(),
         Text = c.Name
        }).ToListAsync();

        _context.Add(clientCreate);
        _context.SaveChanges();
    }

    public void Delete(Client client)
    {
        _context.Clients.Remove(client);
        _context.SaveChanges();
    }

    public Client? GetDetails(int id)
    {
        var client = _context.Clients
        .Include(x=>x.Tickets)
        .FirstOrDefault(m => m.Id == id);

        // var clientDetailviewModel = new ClientDetailViewModel
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
        return client;
    }

public ClientViewModel GetAll(string filter)
    {
        var query = from client in _context.Clients select client;

        var clients = query.Include(x => x.Tickets);

        if (!string.IsNullOrEmpty(filter))
        {
            query = query.Where(x => x.Name.ToLower().Contains(filter) ||
                                    x.Apellido.ToLower().Contains(filter) ||
                                    x.Mail.ToLower().Contains(filter));

        }

        var model = new ClientViewModel();
        model.Clients = query.ToList();
        return model;
    }

public Client? GetClient(int id)
    {
        var client = _context.Clients
        .Include(x=>x.Tickets)
        .FirstOrDefault(m => m.Id == id);
        return client;
    }

    public void Update(Client client, int id)
    {      
        _context.Update(client);
        _context.SaveChanges();           
    }

}