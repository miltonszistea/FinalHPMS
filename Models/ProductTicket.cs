using System.ComponentModel.DataAnnotations;

namespace FinalHPMS.Models
{
    public class ProductTicket
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; } 
        public int TicketId { get; set; }
        public Ticket? Ticket { get; set; } 
    }
}