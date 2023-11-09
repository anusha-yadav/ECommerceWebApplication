using System.ComponentModel.DataAnnotations;

namespace E_Commerce_WebApplication.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }    
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } 
        public virtual Address ShippingAddress { get;set; }
        public virtual Users User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
