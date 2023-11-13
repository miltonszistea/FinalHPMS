
using System.ComponentModel.DataAnnotations;
namespace FinalHPMS.Models;

public class Community
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string CityAndCountry { get; set; }

    [Required]
    public required string Address { get; set; }

    [Required]
    public required string Phone { get; set; }

    [Required]
    public required string Mail { get; set; }
  
    [Required]
    public CommunityType CommunityType { get; set; }
    public List<ProductCommunity>? ProductCommunities { get; set; }

    public List<Ticket>? Tickets { get; set; }

}