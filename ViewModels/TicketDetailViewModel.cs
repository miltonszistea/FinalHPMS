using FinalHPMS.Models;
namespace FinalHPMS.ViewModels;
using System.ComponentModel.DataAnnotations;
public class TicketDetailViewModel{
    public  int Id { get; set; }
    [Display(Name="Fecha y Hora")]
    public  DateTime DateAndHour { get; set; }
    [Display(Name="Total")]
    public int Total { get; set; }
    [Display(Name="Cliente")]
    public  Client Client { get; set; }
    [Display(Name="Método de pago")]
    public PaymentMethod PaymentMethod { get; set; }
    [Display(Name="Producto")]
    public int ProductId {get; set;}
    [Display(Name="Productos")]
    public virtual List<Product> Products {get;set;}
    public int ClientId {get; set;}
    public int CommunityId{get; set;}
    public  Community Community{get; set;}
}







