namespace E_Commerce_WebApplication.Models
{
    public class BuyNowViewModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; set; }
        public virtual Products Product { get; set; }
        public int UserID { get; set; }
        public virtual Users User { get; set; }
    }
}
