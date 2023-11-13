using System.ComponentModel.DataAnnotations;

namespace FinalHPMS.Models
{
    public class ProductCommunity
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int CommunityId { get; set; }
        public Community? Community { get; set; }
    }
}