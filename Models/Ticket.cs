using System.ComponentModel.DataAnnotations;

namespace FinalHPMS.Models;

public class Ticket
{
    [Required]
    [Key]
    public int Id { get; set; }
    public string? Description { get; set; }
    [Required]
    public required DateTime DateAndHour { get; set; }
    [Required]
    public double Total { get; set; }
    [Required]
    public PaymentMethod PaymentMethod { get; set; }
    public int CommunityId { get; set; }
    public Community? Community { get; set; } 
    public int ClientId { get; set; }
    public Client? Client { get; set; } 
    public List<ProductTicket>? ProductTickets { get; set; } 

}