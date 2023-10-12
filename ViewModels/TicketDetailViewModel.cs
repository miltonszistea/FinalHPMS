using FinalHPMS.Models;
namespace FinalHPMS.ViewModels;
using System.ComponentModel.DataAnnotations;
public class TicketDetailViewModel{
    public  int Id { get; set; }
    public  DateTime DateAndHour { get; set; }
    public int Total { get; set; }
    public  Client Client { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public int ProductId {get; set;}
    public virtual List<Product> Products {get;set;}
    public int ClientId {get; set;}
    public int CommunityId{get; set;}
    public  Community Community{get; set;}
}







