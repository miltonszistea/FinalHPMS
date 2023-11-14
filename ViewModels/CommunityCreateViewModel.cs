using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalHPMS.Models;


namespace FinalHPMS.ViewModels
{
    public class CommunityCreateViewModel
    {
        [Display(Name = "Comunidad")]
        public string Name { get; set; }
    
        [Display(Name="Ciudad-País")]
        public string CityAndCountry { get; set; }
        
        [Display(Name="Dirección")]
        public string Address { get; set; }
        
        [Display(Name="Teléfono")]
        public string Phone { get; set; }

        [Display(Name="Mail")]
        public string Mail { get; set; }
  
        [Display(Name="Tipo de Comunidad")]
        public CommunityType CommunityType { get; set; }

        public List<Product>? Products {get; set;}
        // public virtual List<SelectListItem>? Products { get; set; }
        // public List<int>? ProductsIds { get; set; }
        
    }
}