
using System.ComponentModel.DataAnnotations;
namespace FinalHPMS.Models;

public class Community{
    
    [Required]
    public int Id { get; set; }
    
    [Required]
    [Display(Name="Nombre")]
    public required string Name { get; set; }
    
    [Required]
    [Display(Name="Ciudad-País")]
    public required string CityAndCountry { get; set; }
    
    [Required]
    [Display(Name="Dirección")]
    public required string Address { get; set; }
    
    [Required]
    [Display(Name="Teléfono")]
    public required string Phone { get; set; }

    [Required]
    [Display(Name="Mail")]
    public required string Mail { get; set; }
    
    [Required]
    [Display(Name="Tipo de Comunidad")]
    public CommunityType CommunityType { get; set; }

    public int ProductId {get; set;}
    public virtual List<Product> Products {get; set;}
    public int TicketId {get; set;}
    public virtual List<Ticket> Tickets {get; set;}
}