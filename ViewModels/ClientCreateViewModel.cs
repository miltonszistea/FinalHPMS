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
    [StringLength(40, ErrorMessage = "La dirección no puede tener más de 40 caracteres.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "La dirección debe contener solo letras, números y espacios.")]
    [Required]
    public required string Address { get; set; }
    
    [Display(Name="Teléfono")]
    [Required]
    public required string Phone { get; set; }

    [Display(Name="Mail")]
    [Required]
    [EmailAddress]
    public required string Mail { get; set; }

    [Display(Name="Fecha y Hora")]
    [Required]
    public required string DateAndHour { get; set; }
}