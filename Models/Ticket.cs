using System.ComponentModel.DataAnnotations;

namespace FinalHPMS.Models;

public class Ticket
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Display(Name="Descripcion")]
    public string? Description { get; set; }
    
    [Display(Name="Fecha y Hora")]
    [Required]
    public required DateTime DateAndHour { get; set; }
    
    [Required]
    public double Total { get; set; }
    [Required]
    [Display(Name="Método de Pago")]
    public PaymentMethod PaymentMethod { get; set; }
    [Display(Name="Id Comunidad")]
    public int CommunityId { get; set; }
    [Display(Name="Comunidad")]
    public Community? Community { get; set; } 

    [Display(Name="Id Cliente")]
    public int ClientId { get; set; }
    [Display(Name="Cliente")]
    public Client? Client { get; set; } 
    
    public List<ProductTicket>? ProductTickets { get; set; } 

}