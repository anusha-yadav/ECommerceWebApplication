using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce_WebApplication.Models
{
    public class CheckoutViewModel
    {
        public Cart Cart { get; set; }
        public decimal TotalPrice { get; set; }

        // Shipping information
        public Address ShippingAddress { get; set; }
        public PaymentMethod SelectedPaymentOption { get; set; }
        public List<SelectListItem> PaymentOptions
        {
            get; set;
        }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
        public string UPIID { get; set; }
        public bool IsCODSelected { get; set; }
    }
}
