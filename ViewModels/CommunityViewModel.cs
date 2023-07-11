using FinalHPMS.Models;
namespace FinalHPMS.ViewModels;

public class CommunityViewModel{
    public List<Community> Communities{get;set;} = new List<Community>();
    public string? Filter {get; set;}
}