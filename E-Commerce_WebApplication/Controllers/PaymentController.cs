using E_Commerce_WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

namespace E_Commerce_WebApplication.Controllers
{
    public class PaymentController : Controller
    {
        [BindProperty]
        public OrderGateway _payment { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IntiatePayment()
        {
            string key = "rzp_test_ADJqJzO0UWjvnS";
            string secret = "moqvVgTyLwGIhcouJdbuB6Oc";

            Random _random = new Random();
            string Transaction_id = _random.Next(0,1000).ToString();

            Dictionary<string, object> options = new Dictionary<string, object>();

            options.Add("amount", Convert.ToDecimal(_payment.TotalAmount)*100); // amount in the smallest currency unit
            options.Add("receipt", Transaction_id);
            options.Add("currency", "INR");


            RazorpayClient client = new RazorpayClient(key, secret);
            Razorpay.Api.Order order = client.Order.Create(options);

            ViewBag.orderid = order["id"].ToString();

            return View("PaymentGateway", _payment);
        }
    }
}
