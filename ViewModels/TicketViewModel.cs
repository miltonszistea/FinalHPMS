using FinalHPMS.Models;
namespace FinalHPMS.ViewModels;

public class TicketViewModel{
    public List<Ticket> Tickets{get;set;} = new List<Ticket>();
    public string? Filter {get; set;}
}