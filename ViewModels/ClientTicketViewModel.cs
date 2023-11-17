using FinalHPMS.Models;
namespace FinalHPMS.ViewModels;
using System.ComponentModel.DataAnnotations;
public class ClientTicketViewModel{
    [Display(Name="Nombre")]
    public required string Name { get; set; }
    public List<Ticket> Tickets {get; set;}
}