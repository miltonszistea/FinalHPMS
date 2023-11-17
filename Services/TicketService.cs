using FinalHPMS.Data;
using FinalHPMS.Models;
using FinalHPMS.ViewModels;
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
    public void Create(Ticket ticketCreate, List<Product> products)
    {
        _context.Add(ticketCreate);
        _context.SaveChanges();

        foreach (var product in products)
        {

            var existingProduct = _context.Products
                .FirstOrDefault(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                return;
            }

            if(existingProduct.Stock >= product.Quantity)
            {
                existingProduct.Stock -= product.Quantity;
                var productTicket = new ProductTicket
                {
                    ProductId = product.Id,
                    TicketId = ticketCreate.Id,
                };
                _context.Add(productTicket);
            }

        }

        _context.SaveChanges();
    }

    public void Delete(Ticket ticket)
    {
        _context.Tickets.Remove(ticket);
        _context.SaveChanges();
    }

    public Ticket? GetDetails(int id)
    {
        var ticket = _context.Tickets
        .Include(x => x.Client)
        .FirstOrDefault(m => m.Id == id);

        return ticket;
    }

    public TicketViewModel GetAll()
    {
        var query = from ticket in _context.Tickets select ticket;
        var clients = query.Include(x => x.Client);
        var model = new TicketViewModel
        {
            Tickets = clients.ToList()
        };
        return model;
    }
    public TicketViewModel GetAll(string filter)
    {
        IQueryable<Ticket> query = _context.Tickets
       .Include(p => p.ProductTickets)
       .Include(c => c.Client)
       .Include(c => c.Community);

        if (!string.IsNullOrEmpty(filter))
        {
            query = query
                .Where(x => x.Description != null && x.Description.Contains(filter)
            //||   x.Community.Contains(filter)
            );
        }        

        var model = new TicketViewModel();
        model.Tickets = query.ToList();
        return model;
    }

    public Ticket? GetTicket(int id)
    {
        var ticket = _context.Tickets
        .Include(x => x.Community)
        .Include(c => c.Client)
        .Include(pt => pt.ProductTickets)
        .FirstOrDefault(m => m.Id == id);
        return ticket;
    }

    public void Update(Ticket ticket, int id)
    {
        _context.Update(ticket);
        _context.SaveChanges();
    }

        public List<Ticket> GetTicketsByCommunityId(int id)
    {   
        var ticketsDeComunidad = _context.Tickets.Where(ticket => ticket.CommunityId == id).ToList();

        return ticketsDeComunidad;
    }

        public List<Ticket> GetTicketsByProductId(int productId)
        {
            var ticketsDeProducto = (
                from productTicket in _context.ProductTicket
                join ticket in _context.Tickets on productTicket.TicketId equals ticket.Id
                where productTicket.ProductId == productId
                select ticket
            ).ToList();

            return ticketsDeProducto;
        }

        public List<Ticket> GetTicketsByClientId(int clientId)
        {
            var ticketsDeCliente = (
                from ticket in _context.Tickets
                where ticket.ClientId == clientId
                select ticket
            ).ToList();

            return ticketsDeCliente;
        }
}