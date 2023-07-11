using FinalHPMS.Models;
namespace FinalHPMS.ViewModels;

public class ClientViewModel{
    public List<Client> Clients{get;set;} = new List<Client>();
    public string? Filter {get; set;}
}