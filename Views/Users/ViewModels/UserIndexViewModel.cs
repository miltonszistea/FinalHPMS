using System.ComponentModel.DataAnnotations;

public class UserIndexViewModel
{
    [Display(Name="Nombre")]
    public string UserName { get; set; }
    [Display(Name="Correo")]
    public string Email { get; set; }
    [Display(Name="Rol")]
    public string Role { get; set; }
    public string Id { get; set; }
}
