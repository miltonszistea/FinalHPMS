using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalHPMS.Models;

public class ClientCreateViewModel{
    
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
    [Required]
    public required string Mail { get; set; }

    [Display(Name="Fecha y Hora")]
    [Required]
    public required string DateAndHour { get; set; }
}