using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalHPMS.Models;

public class TicketCreateViewModel{
    
   public int Id { get; set; }

[Display(Name = "Total")]
public double Total { get; set; }


[Display(Name = "Fecha y hora")]
public required DateTime DateAndHour { get; set; }

[Display(Name = "Método de pago")]
public PaymentMethod PaymentMethod { get; set; }

[Display(Name = "Productos")]
public List<Product>? Products { get; set; } 
public string? SelectedProducts { get; set; }
[Display(Name="Id de Comunidad")]
public int CommunityId { get; set; }

[Display(Name = "Comunidades")]
public List<Community>? Communities { get; set; } 

[Display(Name = "Tickets")]
public List<Ticket>? Tickets { get; set; } 
[Display(Name="Id de Cliente")]
public int ClientId { get; set; }

[Display(Name = "Clientes")]
public List<Client>? Clients { get; set; } 
[Display(Name="Productos/Tickets")]
public List<ProductTicket>? ProductTickets { get; set; }

[Display(Name="Comunidad")]
public Community? Community { get; set; }

}