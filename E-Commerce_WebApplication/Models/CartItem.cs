namespace E_Commerce_WebApplication.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int ProductsId { get; set; }
        public int? Quantity { get; set; }
        public int CartID { get; set; }
        public virtual Cart Cart {  get; set; }
        public virtual Products Products { get; set; }
    }
}
