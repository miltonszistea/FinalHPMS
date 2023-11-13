using System.ComponentModel.DataAnnotations;

namespace FinalHPMS.Models;

public class Client{   
    
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }   
    [Required]
    public required string Apellido { get; set; }
    [Required]
    public required string Address { get; set; }
    [Required]
    public required string Phone { get; set; }
    [Required]
    public required string Mail { get; set; }   
    public List<Ticket>? Tickets { get; set; } 

}