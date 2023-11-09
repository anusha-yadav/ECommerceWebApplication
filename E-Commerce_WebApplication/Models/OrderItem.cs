using System.ComponentModel.DataAnnotations;

namespace E_Commerce_WebApplication.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }    
        public int ProductId { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        public bool HasRated { get; set; }
        public virtual Order Order { get; set; }    
        public virtual Products Product { get; set; }
    }
}
