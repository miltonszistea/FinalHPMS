using System.ComponentModel.DataAnnotations;

namespace FinalHPMS.Models
{
    public class ProductCommunity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Id Producto")]
        public int ProductId { get; set; }
        [Display(Name="Producto")]
        public Product? Product { get; set; }
        [Display(Name="Id Comunidad")]
        public int CommunityId { get; set; }
        [Display(Name="Comunidad")]
        public Community? Community { get; set; }
    }
}