namespace E_Commerce_WebApplication.Models
{
    public class BuyNowCheckoutViewModel
    {
        public int Id { get; set; }
        public virtual Address ShippingAddress { get; set; }
        public virtual BuyNowViewModel BuyNowItem { get; set; }
    }
}
