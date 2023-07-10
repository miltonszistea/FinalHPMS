
using System.ComponentModel.DataAnnotations;
namespace FinalHPMS.Models;

public class Community{
    
    [Required]
    public int Id { get; set; }

    [Display(Name="Nombre")]
    [Required]
    public required string Name { get; set; }
    
    [Display(Name="Ciudad-País")]
    [Required]
    public required string CityAndCountry { get; set; }
    
    [Display(Name="Dirección")]
    [Required]
    public required string Address { get; set; }
    
    [Display(Name="Teléfono")]
    [Required]
    public required string Phone { get; set; }

    [Display(Name="Mail")]
    [Required]
    public required string Mail { get; set; }
    
    [Display(Name="Tipo de Comunidad")]
    [Required]
    public CommunityType CommunityType { get; set; }

    public int ProductId {get; set;}
    public virtual List<Product> Products {get; set;}
    //public int TicketId {get; set;}
    //public virtual List<Ticket> Tickets {get; set;}
}