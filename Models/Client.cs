
using System.ComponentModel.DataAnnotations;

namespace FinalHPMS.Models;

public class Client{
    
    [Required]
    public int Id { get; set; }

    [Display(Name="Nombre")]
    [Required]
    public string? Name { get; set; }
    
    [Display(Name="Apellido")]
    [Required]
    public required string Apellido { get; set; }
    
    [Display(Name="Dirección")]
    [Required]
    public required string Address { get; set; }
    
    [Display(Name="Teléfono")]
    [Required]
    public required string Phone { get; set; }

    [Display(Name="Mail")]
    //[EmailAddress]
    [Required]
    public required string Mail { get; set; }

    public int? TicketId {get; set;}
    public virtual List<Ticket>? Tickets {get;set;}
}