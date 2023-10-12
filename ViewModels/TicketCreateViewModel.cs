using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalHPMS.Models;

public class TicketCreateViewModel{
    
    [Required]
    public int Id { get; set; }

    [Display(Name="Cliente")]
    [Required]
    public List<Client> Clients{get;set;} = new List<Client>();


    [Display(Name="Total")]
    [Required]
    public int Total { get; set; }
    

    [Display(Name="Fecha y hora")]
    [Required]
    public required DateTime DateAndHour { get; set; }

    [Display(Name="Método de pago")]
    [Required]
    public PaymentMethod PaymentMethod { get; set; }
    public required virtual List<Product>? Products {get;set;}
    public required List<Community> Communities{get; set;}
}