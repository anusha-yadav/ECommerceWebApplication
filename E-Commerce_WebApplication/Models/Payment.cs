namespace E_Commerce_WebApplication.Models
{
    public class Payment
    {
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }
        public string UpiId { get; set; }
        public string SelectedPaymentMethod { get; set; }
    }
}
