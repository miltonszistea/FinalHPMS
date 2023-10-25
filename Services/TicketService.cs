using FinalHPMS.Data;
using FinalHPMS.Models;
using FinalHPMS.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalHPMS.Services;

public class TicketService : ITicketService
{
    private readonly ProductContext _context;
    public TicketService(ProductContext context)
    {
        _context = context;
    }
    public void Create(Ticket ticketCreate)
    {
      var TicketList = _context.Ticket
        .Select(c => new SelectListItem
        {
         Value = c.Id.ToString(),
         Text = c.Client.ToString()
        }).ToListAsync();

        _context.Add(ticketCreate);
        _context.SaveChanges();
    }

    public void Delete(Ticket ticket)
    {
        _context.Ticket.Remove(ticket);
        _context.SaveChanges();
    }

    public Ticket? GetDetails(int id)
    {
        var ticket = _context.Ticket
        .Include(x=>x.Client)
        .FirstOrDefault(m => m.Id == id);

        return ticket;
    }

    public TicketViewModel GetAll(string filter)
    {
        var query = from ticket in _context.Ticket select ticket;

        var communities = query.Include(x=>x.Client);

        if(!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                query = query.Where(x => x.Client.Name.ToLower().Contains(filter) ||
                             x.Id.ToString().Contains(filter));
                
            }

            var model = new TicketViewModel();
            model.Tickets = query.ToList();
            return model;
    }

public Ticket? GetTicket(int id)
    {
        var ticket = _context.Ticket
        .Include(x=>x.Client)
        .FirstOrDefault(m => m.Id == id);
        return ticket;
    }

    public void Update(Ticket ticket, int id)
    {      
        _context.Update(ticket);
        _context.SaveChanges();           
    }
}
