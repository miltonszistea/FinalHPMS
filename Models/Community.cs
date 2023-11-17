
using System.ComponentModel.DataAnnotations;
namespace FinalHPMS.Models;

public class Community
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Display(Name="Nombre")]
    [Required]
    public required string Name { get; set; }

    [Display(Name="Ciudad/País")]
    [Required]
    public required string CityAndCountry { get; set; }

    [Display(Name="Dirección")]
    [Required]
    public required string Address { get; set; }

    [Display(Name="Teléfono")]
    [Required]
    public required string Phone { get; set; }

    [Display(Name="Correo")]
    [Required]
    public required string Mail { get; set; }
  
    [Display(Name="Tipo Comunidad")]
    [Required]
    public CommunityType CommunityType { get; set; }
    public List<ProductCommunity>? ProductCommunities { get; set; }

    public List<Ticket>? Tickets { get; set; }

}