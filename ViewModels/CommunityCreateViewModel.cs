using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalHPMS.Models;


namespace FinalHPMS.ViewModels
{
    public class CommunityCreateViewModel
    {
        [Required]
        [Display(Name = "Comunidad")]
        public string Name { get; set; }
    
        [Required]
        [Display(Name="Ciudad-País")]
        public required string CityAndCountry { get; set; }
        
        [Required]
        [Display(Name="Dirección")]
        public required string Address { get; set; }
        
        [Required]
        [Display(Name="Teléfono")]
        public required string Phone { get; set; }

        [Required]
        [Display(Name="Mail")]
        public required string Mail { get; set; }
        
        [Required]
        [Display(Name="Tipo de Comunidad")]
        public CommunityType CommunityType { get; set; }

        public virtual List<SelectListItem>? Products { get; set; }
        public virtual Product? Product { get; set; }
        public List<int> ProductsIds { get; set; }
        
    }
}