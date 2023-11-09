using E_Commerce_WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_WebApplication.Data;
using System.Diagnostics;
using E_Commerce_WebApplication.Repositories;
using E_Commerce_WebApplication.FactoryPattern;
using E_Commerce_WebApplication.Utilities;
using E_Commerce_WebApplication.Repositories.Interfaces;

namespace E_Commerce_WebApplication.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IUserIdUtility _userIdUtility;

        /// <summary>
        /// Initializing new instances
        /// </summary>
        /// <param name="repositoryFactory"></param>
        /// <param name="userIdUtility"></param>
        public CheckoutController(IRepositoryFactory repositoryFactory, IUserIdUtility userIdUtility)
        {
            _repositoryFactory = repositoryFactory;
            _userIdUtility = userIdUtility;
        }

        /// <summary>
        /// GET://Checkout
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: Checkout
        /// </summary>
        /// <returns></returns>
        public IActionResult Checkout()
        {
            int? userid = _userIdUtility.GetUserId();

            ICheckoutRepository _checkoutRepository = _repositoryFactory.CreateCheckoutRepository();
            IOrderRepository _orderRepository = _repositoryFactory.CreateOrderRepository();

            Cart cart = _checkoutRepository.GetCart(userid.Value);

            var viewModel = new CheckoutViewModel
            {
                Cart = cart,
                TotalPrice = (decimal)cart.CartItems.Sum(item => item.Products.Price * item.Quantity),
                ShippingAddress = new Address()
            };
            return View(viewModel);
        }

        /// <summary>
        /// BuyNow Checkout action method
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public IActionResult BuyNowCheckout(int productId, int userid)
        {
            int? userId = _userIdUtility.GetUserId();

            ICheckoutRepository _checkoutRepository = _repositoryFactory.CreateCheckoutRepository();
            IOrderRepository _orderRepository = _repositoryFactory.CreateOrderRepository();

            if (userId.Value == userid)
            {
                BuyNowViewModel buyNowItem = _checkoutRepository.GetBuyNowItemForCheckout(productId, userId.Value);
                var model = new BuyNowCheckoutViewModel
                {
                    ShippingAddress = new Address(),
                    BuyNowItem = buyNowItem,
                };

                //_context.SaveChanges();
                return View(model);
            }
            return NotFound();
        }

        /// <summary>
        /// BuyNowPayment Action method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult BuyNowPayment(Payment model)
        {
            return View(model);
        }

        /// <summary>
        /// BuyNowOrderConfirmation method
        /// </summary>
        /// <returns></returns>
        public IActionResult BuyNowOrderConfirmation()
        {
            int? userid = _userIdUtility.GetUserId();

            ICheckoutRepository _checkoutRepository = _repositoryFactory.CreateCheckoutRepository();
            IOrderRepository _orderRepository = _repositoryFactory.CreateOrderRepository();

            if (userid.HasValue)
            {

                BuyNowViewModel buyNowItem = _checkoutRepository.GetItemOfUser(userid.Value);

                if (buyNowItem != null)
                {
                    Order order = _orderRepository.CreateOrder(buyNowItem);
                    return View(@"Views/Checkout/BuyNowOrderConfirmation.cshtml", order);
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Process Payment handles payment and orders of the cart
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ProcessPayment(CheckoutViewModel viewModel)
        {
            // Process payment and update order status
            int userId = (int)_userIdUtility.GetUserId();

            ICheckoutRepository _checkoutRepository = _repositoryFactory.CreateCheckoutRepository();
            IOrderRepository _orderRepository = _repositoryFactory.CreateOrderRepository();

            Cart userCart = _checkoutRepository.GetCart(userId);

            // Example: Use a payment service to process the payment
            var paymentservice = new PaymentService();
            var paymentResult = paymentservice.ProcessPayment(viewModel.CardNumber, viewModel.ExpirationDate, viewModel.CVV);

            if (paymentResult.Success)
            {
                Order order = _orderRepository.CreateOrderForAddCart(viewModel, userId, userCart);
                return View("PaymentConfirmation", order);
            }
            ModelState.AddModelError(string.Empty, "Payment failed. Please check your payment information and try again.");
            return View("Checkout", viewModel);
        }
    }
}
