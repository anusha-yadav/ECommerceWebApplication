namespace E_Commerce_WebApplication.Models
{

    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
